[![Build status](https://ci.appveyor.com/api/projects/status/cnptestoiyihl5v8/branch/master?svg=true)](https://ci.appveyor.com/project/dhelper/asserthelper/branch/master)

# AssertHelper

**One assert to rule them all!**

AssertHelper is a fluent assertion library for .NET that simplifies unit testing by allowing you to write assertions using natural C# expressions. Instead of remembering which specific assert method to call, you write intuitive boolean expressions and AssertHelper automatically translates them to the appropriate framework-specific assertions.

## Table of Contents

- [Overview](#overview)
- [Supported Frameworks](#supported-frameworks)
- [Installation](#installation)
- [Usage](#usage)
  - [Basic Assertions](#basic-assertions)
  - [Multiple Assertions](#multiple-assertions)
  - [Advanced Examples](#advanced-examples)
- [Assertion Mappings](#assertion-mappings)
- [Running Tests](#running-tests)
- [Requirements](#requirements)
- [License](#license)

## Overview

AssertHelper provides a unified assertion syntax across different testing frameworks. The library uses expression trees to analyze your boolean expressions and automatically calls the correct underlying assertion method from your chosen testing framework (MSTest or NUnit).

### Key Benefits

- **Intuitive Syntax**: Write assertions as natural boolean expressions
- **Framework Agnostic**: Works seamlessly with both MSTest and NUnit
- **Better Error Messages**: Includes the expression itself in error messages for easier debugging
- **Multiple Assertions**: Run multiple assertions in one call and see all failures, not just the first
- **Type Safety**: Leverage C# compiler checking for your assertions

## Supported Frameworks

AssertHelper currently supports the following unit testing frameworks:

- **MSTest** (Microsoft.VisualStudio.TestTools.UnitTesting)
- **NUnit** (NUnit Framework 2.6.4+)

The library automatically detects which framework you're using and calls the appropriate assertion methods.

## Installation

### Via NuGet Package Manager

```bash
Install-Package AssertHelper
```

### Via .NET CLI

```bash
dotnet add package AssertHelper
```

### Manual Installation

1. Download the `AssertHelper.Core.dll` from the [releases page](https://github.com/dhelper/AssertHelper/releases)
2. Add a reference to the DLL in your test project

## Usage

### Basic Assertions

Add the namespace to your test file:

```csharp
using AssertHelper.Core;
```

Then use `Expect.That()` with lambda expressions:

#### Equality Assertions

```csharp
[Test]
public void TestEquality()
{
    int actual = 42;
    int expected = 42;
    
    // Instead of: Assert.AreEqual(expected, actual);
    Expect.That(() => actual == expected);
}
```

#### Boolean Assertions

```csharp
[Test]
public void TestBooleanValues()
{
    bool isValid = true;
    bool isEnabled = false;
    
    // Instead of: Assert.IsTrue(isValid);
    Expect.That(() => isValid == true);
    
    // Instead of: Assert.IsFalse(isEnabled);
    Expect.That(() => isEnabled == false);
}
```

#### Null Checking

```csharp
[Test]
public void TestNullValues()
{
    object obj = null;
    string text = "Hello";
    
    // Instead of: Assert.IsNull(obj);
    Expect.That(() => obj == null);
    
    // Instead of: Assert.IsNotNull(text);
    Expect.That(() => text != null);
}
```

#### Type Checking

```csharp
[Test]
public void TestInstanceType()
{
    object value = "Hello World";
    
    // Instead of: Assert.IsInstanceOfType(value, typeof(string));
    Expect.That(() => value is string);
}
```

#### String Assertions

```csharp
[Test]
public void TestStringOperations()
{
    string text = "Hello World";
    
    // Instead of: StringAssert.Contains("World", text);
    Expect.That(() => text.Contains("World"));
    
    // Instead of: StringAssert.StartsWith("Hello", text);
    Expect.That(() => text.StartsWith("Hello"));
    
    // Instead of: StringAssert.EndsWith("World", text);
    Expect.That(() => text.EndsWith("World"));
}
```

#### Collection Assertions

```csharp
[Test]
public void TestCollections()
{
    var numbers = new[] { 1, 2, 3, 4, 5 };
    var expected = new[] { 1, 2, 3, 4, 5 };
    
    // Instead of: CollectionAssert.AreEqual(expected, numbers);
    Expect.That(() => numbers == expected);
    
    // Instead of: CollectionAssert.Contains(numbers, 3);
    Expect.That(() => numbers.Contains(3));
}
```

### Multiple Assertions

One of the most powerful features of AssertHelper is the ability to run multiple assertions in a single call. All assertions will be evaluated, and you'll see all failures, not just the first one:

```csharp
[Test]
public void TestMultipleConditions()
{
    int value = 100;
    string text = "Hello";
    bool flag = true;
    
    // All three assertions will be checked
    // If any fail, you'll see all failures in the error message
    Expect.That(() => value == 100 && text != null && flag);
}
```

This is equivalent to:

```csharp
[Test]
public void TestMultipleConditionsTraditional()
{
    int value = 100;
    string text = "Hello";
    bool flag = true;
    
    Assert.AreEqual(100, value);
    Assert.IsNotNull(text);
    Assert.IsTrue(flag);
}
```

But with the advantage that if multiple assertions fail, you'll see all failures at once rather than stopping at the first failure.

### Advanced Examples

#### MSTest Example

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssertHelper.Core;

namespace MyProject.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_TwoNumbers_ReturnsSum()
        {
            // Arrange
            var calculator = new Calculator();
            
            // Act
            int result = calculator.Add(2, 3);
            
            // Assert
            Expect.That(() => result == 5);
        }
        
        [TestMethod]
        public void Calculate_ComplexScenario_AllConditionsMet()
        {
            // Arrange
            var calculator = new Calculator();
            
            // Act
            var result = calculator.Calculate(10, 5);
            
            // Assert - Multiple conditions checked at once
            Expect.That(() => 
                result != null && 
                result.Sum == 15 && 
                result.Difference == 5 &&
                result.IsValid);
        }
    }
}
```

#### NUnit Example

```csharp
using NUnit.Framework;
using AssertHelper.Core;

namespace MyProject.Tests
{
    [TestFixture]
    public class StringProcessorTests
    {
        [Test]
        public void Process_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            var processor = new StringProcessor();
            string input = "Hello World";
            
            // Act
            string result = processor.Process(input);
            
            // Assert
            Expect.That(() => result != null &&
                             result.StartsWith("Processed:") &&
                             result.Contains("Hello") &&
                             result.Length > input.Length);
        }
        
        [Test]
        public void Process_Collection_ContainsExpectedItems()
        {
            // Arrange
            var processor = new StringProcessor();
            
            // Act
            var results = processor.ProcessBatch(new[] { "a", "b", "c" });
            
            // Assert
            Expect.That(() => results.Contains("Processed:a") &&
                             results.Contains("Processed:b") &&
                             results.Contains("Processed:c"));
        }
    }
}
```

## Assertion Mappings

Here's a complete reference of how AssertHelper expressions map to framework-specific assertions:

| Expression | Translates To |
|-----------|---------------|
| `Expect.That(() => value)` | `Assert.IsTrue(value)` |
| `Expect.That(() => value == true)` | `Assert.IsTrue(value)` |
| `Expect.That(() => value == false)` | `Assert.IsFalse(value)` |
| `Expect.That(() => value == null)` | `Assert.IsNull(value)` |
| `Expect.That(() => value != null)` | `Assert.IsNotNull(value)` |
| `Expect.That(() => result == expected)` | `Assert.AreEqual(expected, result)` |
| `Expect.That(() => result != expected)` | `Assert.AreNotEqual(expected, result)` |
| `Expect.That(() => obj is Type)` | `Assert.IsInstanceOfType(obj, typeof(Type))` |
| `Expect.That(() => str.Contains(sub))` | `StringAssert.Contains(sub, str)` |
| `Expect.That(() => str.StartsWith(sub))` | `StringAssert.StartsWith(sub, str)` |
| `Expect.That(() => str.EndsWith(sub))` | `StringAssert.EndsWith(sub, str)` |
| `Expect.That(() => collection == expected)` | `CollectionAssert.AreEqual(expected, collection)` |
| `Expect.That(() => collection != expected)` | `CollectionAssert.AreNotEqual(expected, collection)` |
| `Expect.That(() => collection.Contains(item))` | `CollectionAssert.Contains(collection, item)` |
| `Expect.That(() => a && b && c)` | Runs all assertions independently and reports all failures |

## Running Tests

### MSTest

#### Using Visual Studio Test Explorer

1. Open your solution in Visual Studio
2. Go to **Test** > **Test Explorer**
3. Click **Run All** or select specific tests

#### Using Command Line (vstest.console.exe)

```bash
vstest.console.exe YourTestAssembly.dll
```

#### Using dotnet CLI

```bash
dotnet test
```

### NUnit

#### Using NUnit Console Runner

```bash
nunit-console.exe YourTestAssembly.dll
```

#### Using dotnet CLI

```bash
dotnet test
```

#### Using NUnit GUI Runner

```bash
nunit.exe YourTestAssembly.dll
```

#### Using Visual Studio with NUnit Test Adapter

1. Install the NUnit3TestAdapter NuGet package
2. Open Test Explorer in Visual Studio
3. Run tests as you would with MSTest

## Requirements

### Framework Requirements

- **.NET Framework 4.0** or higher
- Compatible with .NET Standard projects

### Testing Framework Requirements

Choose one of the following:

- **MSTest**: Microsoft.VisualStudio.TestTools.UnitTesting (included with Visual Studio)
- **NUnit**: NUnit 2.6.4 or higher

### Development Requirements

- **Visual Studio 2010** or higher (for development)
- **C# 4.0** or higher

## Project Structure

```
AssertHelper/
├── AssertHelper.Core/              # Core library with expression parsing and translation
├── AssertHelper.Core.Tests/        # Core library unit tests
├── AssertHelper.MSTest.Tests/      # MSTest-specific tests
├── AssertHelper.NUnit.Tests/       # NUnit-specific tests
├── AssertHelper.TestBase/          # Shared test infrastructure
└── AssertHelper.NuGet/             # NuGet package specifications
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Author

Dror Helper

## Links

- **GitHub Repository**: https://github.com/dhelper/AssertHelper
- **NuGet Package**: https://www.nuget.org/packages/AssertHelper
- **Issue Tracker**: https://github.com/dhelper/AssertHelper/issues

---

**Remember**: With AssertHelper, you write assertions the way you think about them!
