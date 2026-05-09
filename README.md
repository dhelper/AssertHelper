[![Build status](https://ci.appveyor.com/api/projects/status/cnptestoiyihl5v8/branch/master?svg=true)](https://ci.appveyor.com/project/dhelper/asserthelper/branch/master)

# AssertHelper

**One assert to rule them all!**

A C# library that provides a unified, expression-based assertion API. Write natural boolean expressions and let AssertHelper automatically translate them into the appropriate framework-specific assertion calls for **NUnit** or **MSTest**.

## Usage

Use `Expect.That` and expressions to tell your unit testing what to check:

| Expression | Translates To |
|---|---|
| `Expect.That(() => value == true)` | `Assert.IsTrue` |
| `Expect.That(() => value == false)` | `Assert.IsFalse` |
| `Expect.That(() => value == null)` | `Assert.IsNull` |
| `Expect.That(() => value != null)` | `Assert.IsNotNull` |
| `Expect.That(() => result == expected)` | `Assert.AreEqual` |
| `Expect.That(() => result != expected)` | `Assert.AreNotEqual` |
| `Expect.That(() => result is expectedType)` | `Assert.IsInstanceOfType` |
| `Expect.That(() => aString.StartsWith(sub))` | `StringAssert.StartsWith` |
| `Expect.That(() => aString.EndsWith(sub))` | `StringAssert.EndsWith` |
| `Expect.That(() => aString.Contains(sub))` | `StringAssert.Contains` |
| `Expect.That(() => aCollection == expected)` | `CollectionAssert.AreEqual` |
| `Expect.That(() => aCollection != expected)` | `CollectionAssert.AreNotEqual` |
| `Expect.That(() => aCollection.Contains(val))` | `CollectionAssert.Contains` |

### Compound Assertions

Run several assertions using one call — they run independently and report all failures:

```csharp
Expect.That(() => a == 100 && b != null);
// → Assert.AreEqual(100, a) | Assert.IsNotNull(b)
```

---

## Architecture

### High-Level Component Diagram

```mermaid
graph TB
    subgraph "Public API"
        Expect["Expect.That(expression)"]
    end

    subgraph "Expression Conversion Pipeline"
        Decompose["Decompose && Expressions"]
        Converters["Expression Converters<br/>(Chain of Responsibility)"]
        Default["Default: AssertTrueAction"]
    end

    subgraph "Assert Builder Layer"
        AB["AssertBuilder<br/>(Facade)"]
        ABF["AssertBuilderFactory<br/>(Auto-Detection)"]
    end

    subgraph "Framework-Specific Factories"
        MSTest["MsTestAssertBuilderFactory"]
        NUnit["NUnitAssertBuilderFactory"]
    end

    subgraph "Assert Methods"
        Unary["Unary Methods<br/>(IsTrue, IsFalse, IsNull, IsNotNull)"]
        Binary["Binary Methods<br/>(AreEqual, AreNotEqual, Contains)"]
        String["String Methods<br/>(Contains, StartsWith, EndsWith)"]
        Collection["Collection Methods<br/>(Contains, AreEqual, AreNotEqual)"]
    end

    Expect --> Decompose
    Decompose --> Converters
    Converters -->|No match| Default
    Converters --> AB
    Default --> AB
    AB --> ABF
    ABF -->|MSTest detected| MSTest
    ABF -->|NUnit detected| NUnit
    MSTest --> Unary
    MSTest --> Binary
    MSTest --> String
    MSTest --> Collection
    NUnit --> Unary
    NUnit --> Binary
    NUnit --> String
    NUnit --> Collection
```

### Class Diagram — Core Components

```mermaid
classDiagram
    class Expect {
        +That(Expression~Func~bool~~) void$
        +All(Action[]) void$
        -GetAllAssertExpressions(Expression) IEnumerable~Expression~Action~~
        -GetAssertExpression(Expression) Expression~Action~
    }

    class IExpressionTypeToAction {
        <<interface>>
        +IsValid(Expression) bool
        +GetAction(Expression) Expression~Action~
    }

    class ExpressionTypeToActionBase {
        <<abstract>>
        #AssertBuilder IAssertBuilder
    }

    class ExpressionTypeToAction~T~ {
        <<abstract>>
        +IsValid(Expression) bool
        +GetAction(Expression) Expression~Action~
        #IsValidInternal(T) bool
        #GetActionInternal(T, string) Expression~Action~
    }

    class IAssertBuilder {
        <<interface>>
        +GetAreEqualAction()
        +GetAreNotEqualAction()
        +GetIsTrueAction()
        +GetIsFalseAction()
        +GetIsInstanceOf()
        +GetIsNullAction()
        +GetIsNotNullAction()
        +GetStringContains()
        +GetCollectionContains()
    }

    class AssertBuilder {
        -_isTrue IUnaryAssertMethod
        -_areEqual IBinaryAssertMethod
        -_stringContains IBinaryAssertMethod
        -_collectionContains IBinaryAssertMethod
    }

    class IAssertBuilderFactory {
        <<interface>>
        +CreateIsTrue()
        +CreateAreEqual()
        +CreateStringContains()
        +CreateCollectionContains()
    }

    class AssertBuilderFactoryBase {
        <<abstract>>
        #AssertType Type
        #StringAssertType Type
        #CollectionAssertType Type
    }

    class AssertBuilderFactory {
        -_assertBuilder IAssertBuilder$
        +GetAssertBuilder() IAssertBuilder$
    }

    Expect --> IExpressionTypeToAction : uses list of
    IExpressionTypeToAction <|.. ExpressionTypeToActionBase
    ExpressionTypeToActionBase <|-- ExpressionTypeToAction~T~
    ExpressionTypeToAction~T~ <|-- BinaryExpressionEquals
    ExpressionTypeToAction~T~ <|-- StringContains
    ExpressionTypeToAction~T~ <|-- IsInstanceOf
    ExpressionTypeToActionBase --> IAssertBuilder
    IAssertBuilder <|.. AssertBuilder
    AssertBuilder --> IAssertBuilderFactory
    IAssertBuilderFactory <|.. AssertBuilderFactoryBase
    AssertBuilderFactoryBase <|-- MsTestAssertBuilderFactory
    AssertBuilderFactoryBase <|-- NUnitAssertBuilderFactory
    AssertBuilderFactory --> IAssertBuilder
```

### Sequence Diagram — Assertion Execution Flow

```mermaid
sequenceDiagram
    participant Test as Unit Test
    participant Expect as Expect.That()
    participant Converters as Expression Converters
    participant ABF as AssertBuilderFactory
    participant AB as AssertBuilder
    participant Factory as Framework Factory
    participant Method as Assert Method
    participant Framework as Test Framework (MSTest/NUnit)

    Test->>Expect: Expect.That(() => result == 42)
    Expect->>Expect: Decompose expression (split on &&)
    
    loop For each sub-expression
        Expect->>Converters: Find matching converter
        Note over Converters: Iterates ordered list:<br/>StringContains, BinaryExpressionEquals,<br/>IsInstanceOf, etc.
        Converters->>Converters: BinaryExpressionEquals.IsValid() → true
        Converters->>ABF: Get AssertBuilder
        ABF->>ABF: Auto-detect framework<br/>(scan loaded assemblies)
        ABF-->>Converters: return AssertBuilder
        Converters->>AB: GetAreEqualAction(expected: 42, actual: result)
        AB->>Factory: CreateAreEqual()
        Factory-->>AB: AreEqual method wrapper
        AB->>Method: Assert(left, right, lambda)
        Method-->>AB: Expression<Action>
        AB-->>Converters: Expression<Action>
        Converters-->>Expect: Expression<Action>
    end

    Expect->>Expect: Compile expression
    Expect->>Framework: Invoke Assert.AreEqual(42, result, "result == 42")
    Framework-->>Test: Pass/Fail
```

### Sequence Diagram — Framework Auto-Detection

```mermaid
sequenceDiagram
    participant ABF as AssertBuilderFactory
    participant Commands as CreateBuilderCommands
    participant AppDomain as AppDomain.Assemblies
    participant NUnitCmd as CreateNUnitBuilderCommand
    participant MSTestCmd as CreateMsTestBuilderCommand
    participant Builder as AssertBuilder

    ABF->>ABF: GetAssertBuilder() called
    alt Builder already cached
        ABF-->>ABF: Return cached _assertBuilder
    else First call
        ABF->>AppDomain: Get loaded assemblies
        AppDomain-->>ABF: Assembly list
        
        ABF->>Commands: Check each ICreateBuilderCommand
        
        alt "nunit.framework" found
            ABF->>NUnitCmd: CreateAssertBuilder()
            NUnitCmd->>Builder: new AssertBuilder(NUnitAssertBuilderFactory)
            Builder-->>ABF: IAssertBuilder
        else "Microsoft.VisualStudio.QualityTools..." found
            ABF->>MSTestCmd: CreateAssertBuilder()
            MSTestCmd->>Builder: new AssertBuilder(MsTestAssertBuilderFactory)
            Builder-->>ABF: IAssertBuilder
        else No framework found
            ABF->>ABF: throw InvalidOperationException
        end
    end
```

---

## Project Structure

```
AssertHelper/
├── AssertHelper.Core/                    # Main library
│   ├── Expect.cs                         # Public entry point
│   ├── ExpressionConverters/             # Expression → Assert mapping
│   │   ├── IExpressionTypeToAction.cs    # Converter interface
│   │   ├── ExpressionTypeToActionBase.cs # Base class
│   │   ├── ExpressionTypeToAction<T>.cs  # Generic typed base
│   │   ├── BinaryExpressionEquals.cs     # == operator handling
│   │   ├── BinaryExpressionNotEquals.cs  # != operator handling
│   │   ├── StringContains.cs            # String.Contains()
│   │   ├── IsInstanceOf.cs             # 'is' operator
│   │   └── ...
│   ├── AssertBuilders/                   # Framework abstraction
│   │   ├── IAssertBuilder.cs            # Builder interface
│   │   ├── AssertBuilder.cs             # Main builder impl
│   │   ├── AssertBuilderFactory.cs      # Auto-detection factory
│   │   ├── AssertBuilderFactoryBase.cs  # Shared factory logic
│   │   ├── MSTest/                      # MSTest support
│   │   ├── NUnit/                       # NUnit support
│   │   └── AssertMethods/               # Method wrappers
│   │       ├── Base/                    # Abstract bases
│   │       ├── AreEqual.cs
│   │       ├── IsTrue.cs
│   │       └── ...
│   └── Extensions/                      # Helper extensions
├── AssertHelper.Core.Tests.MSTest/      # MSTest integration tests
├── AssertHelper.Core.Tests.NUnit/       # NUnit integration tests
└── AssertHelper.sln
```

## Design Patterns

| Pattern | Where Used | Purpose |
|---------|-----------|---------|
| **Chain of Responsibility** | `ExpressionConverters` list in `Expect` | First matching converter handles the expression |
| **Abstract Factory** | `IAssertBuilderFactory` / `AssertBuilderFactoryBase` | Creates framework-specific assert method wrappers |
| **Command** | `ICreateBuilderCommand` | Encapsulates builder creation per framework |
| **Strategy** | `IExpressionTypeToAction` implementations | Different strategies for different expression types |
| **Facade** | `AssertBuilder` | Unified interface over all assertion types |
| **Singleton** | `AssertBuilderFactory._assertBuilder` | Cache the detected builder across calls |

## Supported Frameworks

- **MSTest** — Auto-detected via `Microsoft.VisualStudio.QualityTools.UnitTestFramework` assembly
- **NUnit** — Auto-detected via `nunit.framework` or `nunit.core` assembly
