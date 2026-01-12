# Contributing to AssertHelper

Thank you for your interest in contributing to AssertHelper! This guide will help you get started with development, testing, and submitting contributions.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Environment Setup](#development-environment-setup)
- [Project Structure](#project-structure)
- [Building the Project](#building-the-project)
- [Running Tests](#running-tests)
- [Code Style Guidelines](#code-style-guidelines)
- [Making Changes](#making-changes)
- [Pull Request Process](#pull-request-process)
- [Adding New Expression Converters](#adding-new-expression-converters)
- [Adding Support for New Test Frameworks](#adding-support-for-new-test-frameworks)
- [Testing Your Changes](#testing-your-changes)
- [Documentation](#documentation)

## Code of Conduct

This project follows a code of conduct to ensure a welcoming and inclusive environment:

- **Be respectful** of differing viewpoints and experiences
- **Accept constructive criticism** gracefully
- **Focus on what is best** for the community
- **Show empathy** towards other community members

## Getting Started

### Prerequisites

Before you begin, ensure you have:

- **.NET Framework 4.0** or higher
- **Visual Studio 2012** or later (Community Edition is fine)
  - Alternative: Visual Studio Code with C# extension
  - Alternative: Rider
- **Git** for version control
- **NuGet Package Manager** (usually included with Visual Studio)

### Fork and Clone

1. Fork the repository on GitHub
2. Clone your fork locally:

```bash
git clone https://github.com/YOUR-USERNAME/AssertHelper.git
cd AssertHelper
```

3. Add the upstream repository:

```bash
git remote add upstream https://github.com/dhelper/AssertHelper.git
```

4. Create a branch for your work:

```bash
git checkout -b feature/my-new-feature
```

## Development Environment Setup

### Using Visual Studio

1. **Open the solution:**
   - Open `AssertHelper.sln` in Visual Studio

2. **Restore NuGet packages:**
   - Right-click on the solution in Solution Explorer
   - Select "Restore NuGet Packages"
   - Or use: `Tools > NuGet Package Manager > Package Manager Console`
   - Run: `Update-Package -Reinstall`

3. **Build the solution:**
   - Press `Ctrl+Shift+B` or use `Build > Build Solution`
   - Verify no errors

4. **Run tests:**
   - Open Test Explorer (`Test > Test Explorer`)
   - Click "Run All"
   - All tests should pass

### Using Command Line

```bash
# Restore packages
nuget restore AssertHelper.sln

# Build
msbuild AssertHelper.sln /p:Configuration=Debug

# Run NUnit tests
nunit-console AssertHelper.Core.Tests\bin\Debug\AssertHelper.Core.Tests.dll

# Run MSTest tests
mstest /testcontainer:AssertHelper.MSTest.Tests\bin\Debug\AssertHelper.MSTest.Tests.dll
```

### Using .NET CLI (if targeting newer frameworks)

```bash
dotnet restore
dotnet build
dotnet test
```

## Project Structure

```
AssertHelper/
│
├── AssertHelper.Core/              # Core library (main implementation)
│   ├── AssertBuilders/            # Framework-specific assertion builders
│   │   ├── NUnit/                # NUnit-specific implementations
│   │   ├── MSTest/               # MSTest-specific implementations
│   │   └── AssertMethods/        # Individual assertion method wrappers
│   ├── ExpressionConverters/      # Expression tree analysis and conversion
│   ├── Extensions/                # Helper extensions for expressions and types
│   └── Expect.cs                  # Main public API
│
├── AssertHelper.Core.Tests/       # Core unit tests (uses FakeItEasy for mocking)
│   └── TestHelpers/              # Test utilities and fakes
│
├── AssertHelper.NUnit.Tests/      # NUnit integration tests
│
├── AssertHelper.MSTest.Tests/     # MSTest integration tests
│
├── AssertHelper.TestBase/         # Shared test base classes
│
├── AssertHelper.NuGet/            # NuGet package specification
│
├── README.md                       # Main documentation
├── ARCHITECTURE.md                 # Architecture documentation
├── EXAMPLES.md                     # Usage examples
└── CONTRIBUTING.md                 # This file
```

## Building the Project

### Debug Build

```bash
msbuild AssertHelper.sln /p:Configuration=Debug
```

### Release Build

```bash
msbuild AssertHelper.sln /p:Configuration=Release
```

### Build Specific Project

```bash
msbuild AssertHelper.Core\AssertHelper.Core.csproj /p:Configuration=Debug
```

### Clean Build

```bash
msbuild AssertHelper.sln /t:Clean
msbuild AssertHelper.sln /p:Configuration=Debug
```

## Running Tests

### All Tests

```bash
# NUnit
nunit-console AssertHelper.Core.Tests\bin\Debug\AssertHelper.Core.Tests.dll

# MSTest
mstest /testcontainer:AssertHelper.MSTest.Tests\bin\Debug\AssertHelper.MSTest.Tests.dll
```

### Specific Test Categories

```bash
# Run only Boolean tests
nunit-console AssertHelper.Core.Tests\bin\Debug\AssertHelper.Core.Tests.dll --where "cat==Boolean"

# Run only Collection tests
nunit-console AssertHelper.Core.Tests\bin\Debug\AssertHelper.Core.Tests.dll --where "cat==Collections"

# Available categories: Boolean, Equality, Collections, String, Null, TypeChecking, Multiple, Unit
```

### In Visual Studio

1. Open **Test Explorer** (`Test > Test Explorer`)
2. Click **Run All** or right-click specific tests
3. Use **Group By > Category** to organize tests
4. Filter tests using the search box

## Code Style Guidelines

### C# Coding Conventions

#### Naming Conventions

```csharp
// Classes, interfaces, methods, properties: PascalCase
public class ExpressionConverter { }
public interface IAssertBuilder { }
public void ConvertExpression() { }
public string PropertyName { get; set; }

// Private fields: camelCase with underscore prefix
private readonly IAssertBuilder _assertBuilder;
private static List<IConverter> _converters;

// Local variables, parameters: camelCase
public void Method(int parameterName)
{
    var localVariable = 10;
}

// Constants: PascalCase
private const int MaxRetries = 3;
```

#### Formatting

```csharp
// Braces on new line (Allman style)
public void MyMethod()
{
    if (condition)
    {
        DoSomething();
    }
}

// Single line for simple properties
public string Name { get; set; }

// Expression-bodied members for simple cases
public int Square(int x) => x * x;
public string FullName => $"{FirstName} {LastName}";

// Explicit access modifiers
public class MyClass
{
    private int _field;
    public int Property { get; set; }
    internal void Method() { }
}
```

#### Documentation

**All public APIs must have XML documentation:**

```csharp
/// <summary>
/// Executes an assertion based on the provided boolean expression.
/// </summary>
/// <param name="predicate">A lambda expression representing the assertion condition.</param>
/// <exception cref="AssertionException">Thrown when the assertion fails.</exception>
/// <example>
/// <code>
/// Expect.That(() => result == 42);
/// </code>
/// </example>
public static void That(Expression<Func<bool>> predicate)
{
    // Implementation
}
```

**Internal classes should have summary documentation:**

```csharp
/// <summary>
/// Converts binary equality expressions into AreEqual assertion calls.
/// </summary>
internal class BinaryExpressionEquals : ExpressionTypeToAction<BinaryExpression>
{
    // Implementation
}
```

#### Modern C# Patterns

Use modern C# features where they improve readability:

```csharp
// Expression-bodied members
public bool IsValid(Expression expr) => expr.NodeType == ExpressionType.Equal;

// Pattern matching (C# 7+)
if (expression is BinaryExpression binaryExpr)
{
    ProcessBinary(binaryExpr);
}

// Null-conditional operator
var result = obj?.Property?.NestedProperty;

// String interpolation
var message = $"Expected {expected}, got {actual}";

// Collection initialization
var converters = new List<IConverter>
{
    new StringContains(),
    new BinaryEquals()
};
```

### Testing Conventions

```csharp
[TestFixture]
[Category("Equality")]  // Add appropriate category
[Category("Unit")]
public class EqualityTests
{
    [Test]
    public void MethodName_Scenario_ExpectedBehavior()  // Descriptive test name
    {
        // Arrange
        var expected = 42;
        var calculator = new Calculator();
        
        // Act
        var actual = calculator.Add(20, 22);
        
        // Assert
        Expect.That(() => actual == expected);
    }
}
```

## Making Changes

### Before You Start

1. **Check existing issues:** Look for related issues or discussions
2. **Create an issue:** For significant changes, create an issue first to discuss
3. **Keep it focused:** One feature or fix per pull request
4. **Update documentation:** Update relevant docs if needed

### Workflow

1. **Sync with upstream:**

```bash
git fetch upstream
git checkout master
git merge upstream/master
```

2. **Create a feature branch:**

```bash
git checkout -b feature/add-async-support
```

3. **Make your changes:**
   - Write code following style guidelines
   - Add XML documentation for public APIs
   - Write unit tests
   - Update examples if needed

4. **Test your changes:**

```bash
# Build
msbuild AssertHelper.sln /p:Configuration=Debug

# Run tests
nunit-console AssertHelper.Core.Tests\bin\Debug\AssertHelper.Core.Tests.dll
```

5. **Commit your changes:**

```bash
git add .
git commit -m "Add support for async assertions"
```

Use clear, descriptive commit messages:
- Start with a verb: "Add", "Fix", "Update", "Remove"
- Be specific: "Fix null reference in CollectionEquals converter"
- Reference issues: "Fix #123: Handle null collections"

6. **Push to your fork:**

```bash
git push origin feature/add-async-support
```

## Pull Request Process

### Creating a Pull Request

1. **Go to GitHub** and create a pull request from your fork
2. **Fill in the template:**
   - Description of changes
   - Related issues
   - Testing performed
   - Breaking changes (if any)

### PR Template

```markdown
## Description
Brief description of what this PR does

## Related Issues
Fixes #123
Related to #456

## Changes Made
- Added support for async assertions
- Updated documentation
- Added unit tests

## Testing
- [ ] All existing tests pass
- [ ] New tests added for new features
- [ ] Manual testing performed
- [ ] Documentation updated

## Breaking Changes
None / List any breaking changes

## Checklist
- [ ] Code follows style guidelines
- [ ] XML documentation added for public APIs
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] Commit messages are clear
```

### Review Process

1. **Automated checks:** CI build must pass
2. **Code review:** Maintainer will review your code
3. **Feedback:** Address any requested changes
4. **Approval:** Once approved, your PR will be merged
5. **Cleanup:** Delete your branch after merge

### PR Requirements

- ✅ All tests must pass
- ✅ Code follows style guidelines
- ✅ Public APIs have XML documentation
- ✅ Changes are covered by tests
- ✅ Documentation is updated if needed
- ✅ Commit messages are clear and descriptive

## Adding New Expression Converters

To add support for a new expression pattern:

### 1. Create Converter Class

```csharp
using System;
using System.Linq.Expressions;
using AssertHelper.Core.ExpressionConverters;

namespace AssertHelper.Core.ExpressionConverters
{
    /// <summary>
    /// Converts expressions using the Any() LINQ method to CollectionAssert.
    /// </summary>
    internal class EnumerableAny : ExpressionTypeToAction<MethodCallExpression>
    {
        protected override bool IsValidInternal(MethodCallExpression methodCall)
        {
            // Check if this is an IEnumerable.Any() call
            return methodCall.Method.Name == "Any" &&
                   methodCall.Method.DeclaringType?.Name == "Enumerable";
        }

        protected override Expression<Action> GetActionInternal(
            MethodCallExpression typedExpression,
            string lambdaString)
        {
            var builder = AssertBuilderFactory.GetAssertBuilder();
            
            // Extract the collection and predicate
            var collection = typedExpression.Arguments[0];
            var predicate = typedExpression.Arguments[1];
            
            // Generate appropriate assertion
            return builder.GetCollectionAnyAction(collection, predicate, lambdaString);
        }
    }
}
```

### 2. Register Converter

Add to `Expect` class static constructor:

```csharp
static Expect()
{
    _actionConverters = new List<IExpressionTypeToAction>
    {
        // Add in appropriate priority order
        new EnumerableAny(),        // Your new converter
        new StringContains(),
        new StringStartsWith(),
        // ... rest of converters
    };
}
```

### 3. Add Builder Method (if needed)

If you need a new assertion type, add to `IAssertBuilder`:

```csharp
public interface IAssertBuilder
{
    // Existing methods...
    
    /// <summary>
    /// Creates a collection "any" assertion.
    /// </summary>
    Expression<Action> GetCollectionAnyAction(
        Expression collection, 
        Expression predicate, 
        string lambda);
}
```

### 4. Implement in Builders

Implement in both NUnit and MSTest builders:

```csharp
// In NUnitAssertBuilder
public Expression<Action> GetCollectionAnyAction(
    Expression collection,
    Expression predicate,
    string lambda)
{
    return _collectionAny.Assert(collection, predicate, lambda);
}
```

### 5. Add Tests

```csharp
[TestFixture]
[Category("Collections")]
[Category("Unit")]
public class EnumerableAnyTests : FakeAssertBuilderTests
{
    [Test]
    public void That_CollectionAny_CallsAppropriateAssert()
    {
        var collection = new List<int> { 1, 2, 3 };
        var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();
        var validator = new CallValidator();
        
        A.CallTo(() => fakeBuilder.GetCollectionAnyAction(
            A<Expression>._, A<Expression>._, A<string>._))
            .AddAssertValidation(validator);
        
        Expect.That(() => collection.Any(x => x > 2));
        
        // Verify correct call
        validator.WasAssertCalledWithArguments(collection, /* predicate check */);
    }
}
```

### 6. Update Documentation

Add example to `EXAMPLES.md`:

```markdown
### Collection Any

```csharp
[Test]
public void CollectionHasMatchingElement()
{
    var numbers = new List<int> { 1, 2, 3, 4, 5 };
    
    Expect.That(() => numbers.Any(x => x > 3));
    // Equivalent to: Assert.IsTrue(numbers.Any(x => x > 3))
}
`` `
```

## Adding Support for New Test Frameworks

### 1. Create Factory

```csharp
internal class XUnitAssertBuilderFactory : AssertBuilderFactoryBase
{
    protected override Type AssertType => typeof(Xunit.Assert);
    protected override Type StringAssertType => typeof(Xunit.Assert);
    protected override Type CollectionAssertType => typeof(Xunit.Assert);
    
    public override IBinaryAssertMethod CreateAreEqual()
    {
        return new XUnitAreEqual(AssertType);
    }
    
    public override IBinaryAssertMethod CreateAreNotEqual()
    {
        return new XUnitAreNotEqual(AssertType);
    }
    
    // Implement all factory methods...
}
```

### 2. Create Registration Command

```csharp
internal class CreateXUnitBuilderCommand : ICreateBuilderCommand
{
    public IAssertBuilderFactory Create()
    {
        return new XUnitAssertBuilderFactory();
    }
}
```

### 3. Register in Factory

```csharp
// In AssertBuilderFactory.cs
static AssertBuilderFactory()
{
    _registeredBuilders = new Dictionary<Type, ICreateBuilderCommand>
    {
        { typeof(Xunit.Assert), new CreateXUnitBuilderCommand() },
        // ... existing registrations
    };
}
```

### 4. Create Assert Method Classes

```csharp
internal class XUnitAreEqual : AssertMethodBase, IBinaryAssertMethod
{
    public XUnitAreEqual(Type assertType)
        : base(assertType, "Equal", new[] { typeof(object), typeof(object) })
    {
    }
    
    public Expression<Action> Assert(Expression left, Expression right, string lambda)
    {
        // xUnit uses Equal(expected, actual) without message
        return GetExpression(left, right);
    }
}
```

### 5. Add Integration Tests

Create a new test project:

```
AssertHelper.XUnit.Tests/
├── XUnitTests.cs
└── AssertHelper.XUnit.Tests.csproj
```

### 6. Document Framework Support

Update `README.md`:

```markdown
## Supported Frameworks

- **NUnit** 2.6+
- **MSTest** (Visual Studio Unit Testing Framework)
- **xUnit** 2.0+ (NEW!)
```

## Testing Your Changes

### Unit Tests

All new code should have unit tests:

```csharp
[TestFixture]
[Category("YourFeature")]
[Category("Unit")]
public class YourFeatureTests : FakeAssertBuilderTests
{
    [Test]
    public void Method_Scenario_ExpectedBehavior()
    {
        // Arrange
        var sut = CreateSystemUnderTest();
        
        // Act
        var result = sut.Method();
        
        // Assert
        Expect.That(() => result == expected);
    }
}
```

### Integration Tests

Test with real frameworks:

```csharp
[TestFixture]
public class IntegrationTests
{
    [Test]
    public void ActualFramework_RealAssertion_Works()
    {
        // This should actually execute the framework assertion
        Expect.That(() => 2 + 2 == 4);
    }
    
    [Test]
    public void FailingAssertion_ThrowsException()
    {
        // Verify that failing assertions throw
        Assert.Throws<AssertionException>(() =>
        {
            Expect.That(() => 2 + 2 == 5);
        });
    }
}
```

### Manual Testing

1. Create a test console app
2. Reference AssertHelper.Core
3. Write manual test cases
4. Verify behavior matches expectations

## Documentation

### When to Update Documentation

Update documentation when:
- Adding new features
- Changing public APIs
- Adding new expression patterns
- Adding framework support
- Fixing significant bugs

### Documentation Files

- **README.md**: Update usage examples, feature list, installation
- **ARCHITECTURE.md**: Update if changing internal design
- **EXAMPLES.md**: Add examples for new features
- **CONTRIBUTING.md**: Update if changing development process

### XML Documentation

All public APIs require XML documentation:

```csharp
/// <summary>
/// Brief description of what this does.
/// </summary>
/// <param name="parameter">Description of parameter.</param>
/// <returns>Description of return value.</returns>
/// <exception cref="ExceptionType">When this exception is thrown.</exception>
/// <remarks>
/// Additional details, usage notes, or important information.
/// </remarks>
/// <example>
/// <code>
/// // Usage example
/// MyMethod(value);
/// </code>
/// </example>
```

## Questions?

If you have questions:

1. **Check existing documentation**: README.md, ARCHITECTURE.md, EXAMPLES.md
2. **Search issues**: Someone may have asked before
3. **Create an issue**: Ask your question as a new issue
4. **Discussion**: Use GitHub Discussions for open-ended questions

## License

By contributing, you agree that your contributions will be licensed under the same license as the project (MIT License).

---

**Thank you for contributing to AssertHelper!** 🎉

Your contributions help make unit testing easier for developers everywhere.
