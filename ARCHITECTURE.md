# AssertHelper Architecture

This document describes the internal architecture of AssertHelper, its core components, and how to extend the library.

## Table of Contents

- [Overview](#overview)
- [Core Components](#core-components)
- [Component Interactions](#component-interactions)
- [Flow Diagram](#flow-diagram)
- [Extension Points](#extension-points)
- [Design Patterns](#design-patterns)
- [Testing Strategy](#testing-strategy)

## Overview

AssertHelper uses expression tree analysis to convert C# lambda expressions into test framework-specific assertion calls. The architecture is designed to be:

- **Extensible**: Easy to add support for new expression patterns and test frameworks
- **Decoupled**: Clear separation between expression analysis and assertion generation
- **Testable**: Components are independently testable
- **Maintainable**: Single Responsibility Principle applied throughout

## Core Components

### 1. Main API (`Expect` class)

**Location:** `AssertHelper.Core/Expect.cs`

**Responsibilities:**
- Primary entry point for users
- Orchestrates expression analysis and assertion execution
- Handles multiple assertions via `AndAlso` operator splitting
- Manages error collection and reporting

**Key Methods:**
```csharp
public static void That(Expression<Func<bool>> predicate)
public static void All(params Action[] assertionsToRun)
```

**Design Notes:**
- Static class for convenient access
- Uses comprehensive XML documentation for IntelliSense support
- Implements stack trace cleanup for cleaner error messages

### 2. Expression Converters

**Location:** `AssertHelper.Core/ExpressionConverters/`

**Responsibilities:**
- Analyze expression trees to determine assertion type
- Convert expressions into appropriate assertion method calls
- Priority-based converter chain for expression matching

#### Base Interfaces and Classes

```csharp
// Core converter interface
internal interface IExpressionTypeToAction
{
    bool IsValid(Expression expr);
    Expression<Action> GetAction(Expression expression);
}

// Generic base implementation
internal abstract class ExpressionTypeToAction<T> : ExpressionTypeToActionBase
    where T : Expression
```

#### Converter Types

| Converter | Expression Pattern | Output |
|-----------|-------------------|--------|
| `BinaryExpressionEquals` | `a == b` | AreEqual |
| `BinaryExpressionNotEquals` | `a != b` | AreNotEqual |
| `BinaryExpressionWithConstantRightAction` | `value == true/false/null` | IsTrue/IsFalse/IsNull |
| `BinaryExpressionWithConstantLeftAction` | `true/false/null == value` | IsTrue/IsFalse/IsNull |
| `UnaryNotExpressionToAction` | `!value` | IsFalse |
| `IsInstanceOf` | `obj is Type` | IsInstanceOfType |
| `StringContains` | `str.Contains(x)` | StringAssert.Contains |
| `StringStartsWith` | `str.StartsWith(x)` | StringAssert.StartsWith |
| `StringEndsWith` | `str.EndsWith(x)` | StringAssert.EndsWith |
| `EnumerableEquals` | `enumerable == other` | CollectionAssert.AreEqual |
| `EnumerableContains` | `enumerable.Contains(x)` | CollectionAssert.Contains |
| `CollectionContains` | `collection.Contains(x)` | CollectionAssert.Contains |
| `AssertTrueAction` | Any other boolean | IsTrue (fallback) |

**Converter Priority:**

Converters are registered in priority order. The first converter where `IsValid()` returns true is used:

1. String operations (most specific)
2. Unary operations (!)
3. Binary with constants (null/true/false checks)
4. Collection operations
5. Equality/inequality
6. Type checking
7. IsTrue (fallback)

### 3. Assert Builders

**Location:** `AssertHelper.Core/AssertBuilders/`

**Responsibilities:**
- Abstract test framework differences
- Generate framework-specific assertion expressions
- Handle parameter marshalling and type conversion

#### Core Interfaces

```csharp
// Main builder interface
public interface IAssertBuilder
{
    Expression<Action> GetAreEqualAction(Expression left, Expression right, string lambda);
    Expression<Action> GetAreNotEqualAction(Expression left, Expression right, string lambda);
    Expression<Action> GetIsTrueAction(Expression expression, string lambda);
    Expression<Action> GetIsFalseAction(Expression expression, string lambda);
    // ... more methods
}

// Factory for creating builders
public interface IAssertBuilderFactory
{
    IBinaryAssertMethod CreateAreEqual();
    IBinaryAssertMethod CreateAreNotEqual();
    // ... more factory methods
}
```

#### Framework Implementations

**NUnit Implementation:**
- `NUnitAssertBuilderFactory`: Creates NUnit-specific assert methods
- Uses `typeof(NUnit.Framework.Assert)` and `typeof(NUnit.Framework.CollectionAssert)`

**MSTest Implementation:**
- `MsTestAssertBuilderFactory`: Creates MSTest-specific assert methods  
- Uses `typeof(Microsoft.VisualStudio.TestTools.UnitTesting.Assert)` and `CollectionAssert`

**Factory Registration:**

```csharp
public class AssertBuilderFactory
{
    private static readonly Dictionary<Type, ICreateBuilderCommand> _registeredBuilders;
    
    static AssertBuilderFactory()
    {
        _registeredBuilders = new Dictionary<Type, ICreateBuilderCommand>
        {
            { typeof(NUnit.Framework.Assert), new CreateNunitBuilderCommand() },
            { typeof(Microsoft.VisualStudio.TestTools.UnitTesting.Assert), new CreateMsTestBuilderCommand() }
        };
    }
}
```

The factory automatically detects the test framework by checking which Assert type is available in the current AppDomain.

### 4. Assert Methods

**Location:** `AssertHelper.Core/AssertBuilders/AssertMethods/`

**Responsibilities:**
- Encapsulate specific assertion method calls
- Handle type conversions for parameters
- Generate method call expressions

#### Method Types

**Base Interfaces:**
```csharp
internal interface IUnaryAssertMethod
{
    Expression<Action> Assert(Expression expression, string lambda);
}

internal interface IBinaryAssertMethod
{
    Expression<Action> Assert(Expression left, Expression right, string lambda);
}

internal interface IBinaryAssertMethod<TLeft, TRight>
{
    Expression<Action> Assert(TLeft left, TRight right, string lambda);
}
```

**Examples:**

```csharp
// Simple unary assertion
internal class IsTrue : UnaryAssertMethodBase
{
    public IsTrue(Type assertType) 
        : base(assertType, "IsTrue", new[] { typeof(bool), typeof(string) })
    {
    }
}

// Binary assertion with type conversion
internal class CollectionAreEqualWithIEnumerable : AssertMethodBase, IBinaryAssertMethod
{
    public Expression<Action> Assert(Expression left, Expression right, string lambda)
    {
        return GetExpression(
            Expression.Convert(left, typeof(IEnumerable)), 
            Expression.Convert(right, typeof(IEnumerable)), 
            lambda.ToConstantExpression());
    }
}
```

### 5. Extensions

**Location:** `AssertHelper.Core/Extensions/`

**Responsibilities:**
- Helper methods for expression manipulation
- Pretty-printing of expressions for error messages
- Type checking utilities

**Key Extensions:**

```csharp
// ExpressionExtensions
public static string PrettyPrint(this Expression expression)
public static ConstantExpression ToConstantExpression(this string value)

// TypeExtensions
public static bool IsEnumerable(this Type type)
public static bool IsCollection(this Type type)

// MethodInfoExtensions
public static Expression<Action> CreateActionExpression(this MethodInfo methodInfo, params Expression[] parameters)
```

## Component Interactions

### High-Level Flow

```
User Code
   ↓
Expect.That(λ)
   ↓
Parse Expression Tree
   ↓
Split on AndAlso? → Yes → Multiple Assertions → Expect.All()
   ↓ No                                              ↓
Single Assertion                              Collect Exceptions
   ↓                                                 ↓
Find Matching Converter                       Report All Failures
   ↓
Get IAssertBuilder
   ↓
Generate Framework-Specific Expression
   ↓
Compile & Execute
   ↓
Pass/Fail
```

### Detailed Interaction Sequence

1. **User calls `Expect.That(() => x == y)`**

2. **Expression Analysis:**
   ```csharp
   var expression = predicate.Body;  // BinaryExpression with NodeType.Equal
   ```

3. **Check for Multiple Assertions:**
   ```csharp
   if (expression.NodeType == ExpressionType.AndAlso)
   {
       // Split and recurse
       var assertions = GetAllAssertExpressions(expression);
       All(assertions.Select(e => e.Compile()).ToArray());
   }
   ```

4. **Find Converter:**
   ```csharp
   var converter = _actionConverters.FirstOrDefault(c => c.IsValid(expression));
   // e.g., BinaryExpressionEquals.IsValid() returns true for '==' operator
   ```

5. **Get Assertion Builder:**
   ```csharp
   var builder = AssertBuilderFactory.GetAssertBuilder();
   // Returns NUnitAssertBuilder or MsTestAssertBuilder based on available framework
   ```

6. **Generate Expression:**
   ```csharp
   var assertExpression = builder.GetAreEqualAction(leftExpr, rightExpr, "x == y");
   // Returns: () => Assert.AreEqual(y, x, "x == y")
   ```

7. **Compile & Execute:**
   ```csharp
   assertExpression.Compile().Invoke();
   ```

## Flow Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                      User Test Code                         │
│                 Expect.That(() => expr)                     │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│                    Expect Class                             │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  1. Extract expression body from lambda              │  │
│  │  2. Check for AndAlso splitting                      │  │
│  │  3. For each sub-expression:                         │  │
│  │     - Call GetAssertExpression()                     │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│              Expression Converters (Chain)                  │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  For each converter in priority order:               │  │
│  │    if (converter.IsValid(expression))                │  │
│  │      return converter.GetAction(expression)          │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│            Selected Converter                               │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  Example: BinaryExpressionEquals                     │  │
│  │  1. Analyze left & right operands                    │  │
│  │  2. Get AssertBuilder from factory                   │  │
│  │  3. Call builder.GetAreEqualAction(left, right)      │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│          AssertBuilderFactory                               │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  Detect test framework:                              │  │
│  │  - Check for NUnit types → NUnitAssertBuilder        │  │
│  │  - Check for MSTest types → MsTestAssertBuilder      │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│        Framework-Specific AssertBuilder                     │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  Example: NUnitAssertBuilder                         │  │
│  │  Creates assert method wrapper:                      │  │
│  │    new AreEqual(typeof(NUnit.Framework.Assert))      │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│              Assert Method Wrapper                          │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  Builds Expression<Action>:                          │  │
│  │  () => NUnit.Framework.Assert.AreEqual(              │  │
│  │           expected, actual, message)                 │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│           Compile & Execute Expression                      │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  expression.Compile().Invoke()                       │  │
│  │  → Executes actual test framework assertion          │  │
│  └──────────────────────────────────────────────────────┘  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
                  ┌──────┴──────┐
                  │             │
                  ▼             ▼
              ┌───────┐    ┌────────┐
              │ PASS  │    │  FAIL  │
              └───────┘    └────────┘
                              │
                              ▼
                    ┌──────────────────┐
                    │ Exception Thrown │
                    │  by Framework    │
                    └──────────────────┘
```

## Extension Points

### Adding Support for New Expression Patterns

To add support for a new expression pattern:

1. **Create a new converter class:**

```csharp
internal class MyNewConverter : ExpressionTypeToAction<MethodCallExpression>
{
    protected override bool IsValidInternal(MethodCallExpression methodCall)
    {
        // Check if this is the pattern you want to handle
        return methodCall.Method.Name == "MyMethod" && 
               methodCall.Method.DeclaringType == typeof(MyType);
    }

    protected override Expression<Action> GetActionInternal(
        MethodCallExpression typedExpression, 
        string lambdaString)
    {
        var builder = AssertBuilderFactory.GetAssertBuilder();
        
        // Extract relevant parts from the expression
        var target = typedExpression.Object;
        var argument = typedExpression.Arguments[0];
        
        // Use appropriate builder method
        return builder.GetMyCustomAction(target, argument, lambdaString);
    }
}
```

2. **Register in Expect static constructor:**

```csharp
static Expect()
{
    _actionConverters = new List<IExpressionTypeToAction>
    {
        new MyNewConverter(),  // Add at appropriate priority
        new StringContains(),
        // ... rest of converters
    };
}
```

3. **Add builder method if needed:**

If the assertion requires a new type of framework call, add it to `IAssertBuilder`:

```csharp
public interface IAssertBuilder
{
    // Existing methods...
    
    Expression<Action> GetMyCustomAction(Expression target, Expression arg, string lambda);
}
```

Then implement in both `NUnitAssertBuilder` and `MsTestAssertBuilder`.

### Adding Support for New Test Frameworks

To add support for a new test framework (e.g., xUnit):

1. **Create factory implementation:**

```csharp
internal class XUnitAssertBuilderFactory : AssertBuilderFactoryBase
{
    protected override Type AssertType => typeof(Xunit.Assert);
    
    protected override Type StringAssertType => typeof(Xunit.Assert); // xUnit doesn't separate
    
    protected override Type CollectionAssertType => typeof(Xunit.Assert);
    
    public override IBinaryAssertMethod CreateAreEqual()
    {
        return new AreEqual(AssertType);
    }
    
    // Implement other factory methods...
}
```

2. **Create registration command:**

```csharp
internal class CreateXUnitBuilderCommand : ICreateBuilderCommand
{
    public IAssertBuilderFactory Create()
    {
        return new XUnitAssertBuilderFactory();
    }
}
```

3. **Register in AssertBuilderFactory:**

```csharp
static AssertBuilderFactory()
{
    _registeredBuilders = new Dictionary<Type, ICreateBuilderCommand>
    {
        { typeof(Xunit.Assert), new CreateXUnitBuilderCommand() },
        { typeof(NUnit.Framework.Assert), new CreateNunitBuilderCommand() },
        { typeof(Microsoft.VisualStudio.TestTools.UnitTesting.Assert), new CreateMsTestBuilderCommand() }
    };
}
```

4. **Create framework-specific assert methods if needed:**

Some frameworks may have different signatures. Create custom assert method classes:

```csharp
internal class XUnitAreEqual : AssertMethodBase, IBinaryAssertMethod
{
    public XUnitAreEqual(Type assertType)
        : base(assertType, "Equal", new[] { typeof(object), typeof(object) }) // Note: xUnit uses "Equal" not "AreEqual"
    {
    }
    
    public Expression<Action> Assert(Expression left, Expression right, string lambda)
    {
        // xUnit expects (expected, actual) without message parameter
        return GetExpression(left, right);
    }
}
```

### Custom Assert Methods

You can also extend the assert method types for framework-specific features:

```csharp
// Example: NUnit has Assert.That with constraint model
internal class NUnitConstraintAssert : AssertMethodBase, IBinaryAssertMethod
{
    public NUnitConstraintAssert(Type assertType)
        : base(assertType, "That", new[] { typeof(object), typeof(IConstraint), typeof(string) })
    {
    }
    
    public Expression<Action> Assert(Expression actualValue, Expression constraint, string lambda)
    {
        return GetExpression(actualValue, constraint, lambda.ToConstantExpression());
    }
}
```

## Design Patterns

### 1. Strategy Pattern

**Where:** Expression Converters

Each converter implements `IExpressionTypeToAction`, providing different strategies for analyzing and converting expressions.

### 2. Factory Pattern

**Where:** AssertBuilderFactory, IAssertBuilderFactory

Factories abstract the creation of framework-specific builders and assertion methods.

### 3. Chain of Responsibility

**Where:** Converter selection in `GetAssertExpression()`

Converters are tried in sequence until one accepts the expression.

### 4. Template Method

**Where:** `ExpressionTypeToAction<T>` base class

Provides common structure (`IsValid`, `GetAction`) while allowing subclasses to customize specific steps.

### 5. Builder Pattern

**Where:** AssertBuilder and Assert Method classes

Gradually builds up expression trees representing method calls.

### 6. Adapter Pattern

**Where:** Framework-specific factories

Adapt different test framework APIs to a common interface.

## Testing Strategy

### Unit Tests

**Location:** `AssertHelper.Core.Tests/`

**Approach:**
- Uses **FakeItEasy** to mock `IAssertBuilder`
- Tests converters independently
- Verifies correct builder methods are called with correct arguments

**Example:**
```csharp
[Test]
public void That_PassTwoValuesWithEqualSign_TransformToAssertEqual()
{
    var expected = 5;
    var actual = 10;
    
    var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();
    var validator = new CallValidator();
    
    A.CallTo(() => fakeBuilder.GetAreEqualAction(
        A<Expression>._, A<Expression>._, A<string>._))
        .AddAssertValidation(validator);
    
    Expect.That(() => actual == expected);
    
    validator.WasAssertCalledWithArguments(expected, actual);
}
```

### Integration Tests

**Location:** `AssertHelper.NUnit.Tests/`, `AssertHelper.MSTest.Tests/`

**Approach:**
- Test with actual test frameworks
- Verify assertions pass/fail as expected
- Test error messages

### Test Organization

Tests are organized by category using `[Category]` attributes:

- **Unit** - All core tests
- **Boolean** - Boolean assertion tests (IsTrue, IsFalse)
- **Equality** - Equality/inequality tests
- **Null** - Null checking tests
- **String** - String assertion tests
- **Collections** - Collection assertion tests
- **TypeChecking** - Type checking tests (is operator)
- **Multiple** - Multiple assertion tests

Run specific categories:
```bash
nunit-console --where "cat==Boolean" AssertHelper.Core.Tests.dll
nunit-console --where "cat==Unit" AssertHelper.Core.Tests.dll
```

## Performance Considerations

### Expression Tree Compilation Overhead

Expression trees are compiled to delegates on first use. For frequently executed tests:

- **Cost:** ~1-10ms for initial compilation per unique expression
- **Benefit:** After compilation, near-native execution speed
- **Mitigation:** Not an issue for unit tests; compilation happens once per test method

### Memory Usage

- Each converter is a singleton (created once in static constructor)
- Expression trees are created per assertion but garbage collected immediately after execution
- Minimal heap allocation impact

### Optimization Opportunities

1. **Converter Caching:** Cache compiled converters per expression pattern
2. **Expression Reuse:** Reuse common expression fragments
3. **Lazy Loading:** Load framework-specific types only when needed

## Future Enhancements

### Potential Extensions

1. **Async Support:**
   ```csharp
   await Expect.ThatAsync(() => await GetValueAsync() == expected);
   ```

2. **Custom Constraint DSL:**
   ```csharp
   Expect.That(() => value, Is.GreaterThan(5).And.LessThan(10));
   ```

3. **Performance Assertions:**
   ```csharp
   Expect.That(() => SlowMethod()).CompletesWithin(TimeSpan.FromSeconds(1));
   ```

4. **Collection Assertions:**
   ```csharp
   Expect.That(() => collection.All(x => x > 0));
   Expect.That(() => collection.Any(x => x == target));
   ```

5. **Exception Assertions:**
   ```csharp
   Expect.That(() => method()).Throws<InvalidOperationException>();
   ```

## Conclusion

AssertHelper's architecture is designed for extensibility and maintainability. The clear separation of concerns between expression analysis, framework abstraction, and assertion generation makes it straightforward to add new features, support new expression patterns, and integrate additional test frameworks.

The use of expression trees provides compile-time safety while maintaining flexibility, and the factory/strategy patterns ensure the codebase remains clean and testable.
