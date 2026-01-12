[![Build status](https://ci.appveyor.com/api/projects/status/cnptestoiyihl5v8/branch/master?svg=true)](https://ci.appveyor.com/project/dhelper/asserthelper/branch/master)

# AssertHelper

**One assert to rule them all!**

AssertHelper simplifies unit testing by providing a unified assertion API that works seamlessly with your favorite .NET testing frameworks. Write assertions using natural C# expressions and let AssertHelper automatically convert them to the appropriate framework-specific assertions.

## Features

- ✅ **Unified Syntax** - Write assertions once, work with multiple test frameworks
- ✅ **Expression-Based** - Use natural C# expressions instead of remembering specific assertion methods
- ✅ **Multiple Framework Support** - Works with NUnit and MSTest
- ✅ **Smart Detection** - Automatically determines the best assertion method based on your expression
- ✅ **Multiple Assertions** - Run several assertions independently and see all failures at once
- ✅ **Better Error Messages** - Clear, concise error messages with stack trace cleanup
- ✅ **Zero Learning Curve** - If you know C# expressions, you already know AssertHelper

## Supported Frameworks

- **NUnit** 2.6+
- **MSTest** (Visual Studio Unit Testing Framework)

## Installation

### Via NuGet Package Manager

```powershell
Install-Package AssertHelper
```

### Via .NET CLI

```bash
dotnet add package AssertHelper
```

### Via Package Reference

Add to your `.csproj` file:

```xml
<PackageReference Include="AssertHelper" Version="1.0.5" />
```

## Quick Start

### Basic Usage

```csharp
using AssertHelper.Core;
using NUnit.Framework;  // or MSTest

[TestFixture]
public class CalculatorTests
{
    [Test]
    public void Add_TwoNumbers_ReturnsSum()
    {
        var calculator = new Calculator();
        var result = calculator.Add(2, 3);
        
        // Instead of: Assert.AreEqual(5, result);
        Expect.That(() => result == 5);
    }
    
    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        var calculator = new Calculator();
        
        // Multiple assertions - all run independently
        Expect.That(() => calculator.IsValid && 
                          calculator.Name != null && 
                          calculator.Precision > 0);
    }
}
```

### Expression to Assertion Mapping

AssertHelper analyzes your lambda expressions and converts them to the most appropriate assertion:

| Expression | Converts To | Example |
|------------|-------------|---------|
| `value == true` | `Assert.IsTrue` | `Expect.That(() => isValid == true)` |
| `value == false` | `Assert.IsFalse` | `Expect.That(() => isEmpty == false)` |
| `value == null` | `Assert.IsNull` | `Expect.That(() => obj == null)` |
| `value != null` | `Assert.IsNotNull` | `Expect.That(() => result != null)` |
| `result == expected` | `Assert.AreEqual` | `Expect.That(() => actual == expected)` |
| `result != expected` | `Assert.AreNotEqual` | `Expect.That(() => actual != unwanted)` |
| `obj is ExpectedType` | `Assert.IsInstanceOfType` | `Expect.That(() => result is string)` |
| `str.StartsWith(prefix)` | `StringAssert.StartsWith` | `Expect.That(() => text.StartsWith("Hello"))` |
| `str.EndsWith(suffix)` | `StringAssert.EndsWith` | `Expect.That(() => text.EndsWith(".txt"))` |
| `str.Contains(substring)` | `StringAssert.Contains` | `Expect.That(() => message.Contains("error"))` |
| `collection == expected` | `CollectionAssert.AreEqual` | `Expect.That(() => actualList == expectedList)` |
| `collection != expected` | `CollectionAssert.AreNotEqual` | `Expect.That(() => list1 != list2)` |
| `collection.Contains(item)` | `CollectionAssert.Contains` | `Expect.That(() => list.Contains(42))` |

## Why AssertHelper?

### Without AssertHelper

```csharp
[Test]
public void UserValidation_WithMultipleConditions()
{
    var user = GetUser();
    
    Assert.IsNotNull(user);
    Assert.AreEqual("John", user.Name);
    Assert.IsTrue(user.Age > 18);
    Assert.IsNotNull(user.Email);
    StringAssert.Contains("@", user.Email);
}
```

**Problem:** If the first assertion fails, you don't see the other failures.

### With AssertHelper

```csharp
[Test]
public void UserValidation_WithMultipleConditions()
{
    var user = GetUser();
    
    Expect.That(() => user != null && 
                      user.Name == "John" && 
                      user.Age > 18 && 
                      user.Email != null && 
                      user.Email.Contains("@"));
}
```

**Benefit:** All assertions run independently. You see all failures at once!

### Comparison with Standard Assertions

| Aspect | Standard Assertions | AssertHelper |
|--------|-------------------|-------------|
| **Syntax** | `Assert.AreEqual(expected, actual)` | `Expect.That(() => actual == expected)` |
| **Framework Switching** | Rewrite all assertions | Change using statement |
| **Multiple Assertions** | Stop at first failure | Report all failures |
| **Expression Types** | Remember specific methods | Use natural C# expressions |
| **Null Checks** | `Assert.IsNull()` / `Assert.IsNotNull()` | `obj == null` / `obj != null` |
| **String Assertions** | `StringAssert.Contains()` | `str.Contains()` |
| **Collection Assertions** | `CollectionAssert.Contains()` | `collection.Contains()` |

## API Reference

### Core API

#### `Expect.That(Expression<Func<bool>> predicate)`

Executes an assertion based on the provided boolean expression.

**Parameters:**
- `predicate` - A lambda expression representing the assertion condition

**Example:**
```csharp
Expect.That(() => result == 42);
Expect.That(() => user.IsValid && user.Age >= 18);
```

#### `Expect.All(params Action[] assertionsToRun)`

Executes multiple assertion actions independently and reports all failures.

**Parameters:**
- `assertionsToRun` - Array of assertion actions to execute

**Example:**
```csharp
Expect.All(
    () => Assert.AreEqual(5, calculator.Add(2, 3)),
    () => Assert.AreEqual(1, calculator.Subtract(3, 2)),
    () => Assert.AreEqual(6, calculator.Multiply(2, 3))
);
```

## Advanced Usage

### Complex Boolean Logic

```csharp
// All conditions are evaluated independently
Expect.That(() => 
    user.FirstName != null &&
    user.LastName != null &&
    user.Email.Contains("@") &&
    user.Age >= 18 &&
    user.IsActive == true);

// If multiple conditions fail, you'll see all failures in one report
```

### Custom Error Context

The lambda expression itself serves as documentation:

```csharp
// Clear what's being tested
Expect.That(() => order.TotalAmount == expectedTotal);

// vs unclear
Assert.AreEqual(expectedTotal, order.TotalAmount);
```

## Building from Source

### Prerequisites

- .NET Framework 4.0 or higher
- Visual Studio 2012+ or MSBuild

### Build Steps

1. Clone the repository:
```bash
git clone https://github.com/dhelper/AssertHelper.git
cd AssertHelper
```

2. Restore NuGet packages:
```bash
nuget restore AssertHelper.sln
```

3. Build the solution:
```bash
msbuild AssertHelper.sln /p:Configuration=Release
```

4. Run tests:
```bash
# NUnit tests
nunit-console AssertHelper.Core.Tests\bin\Release\AssertHelper.Core.Tests.dll

# MSTest tests
mstest /testcontainer:AssertHelper.MSTest.Tests\bin\Release\AssertHelper.MSTest.Tests.dll
```

### Project Structure

```
AssertHelper/
├── AssertHelper.Core/           # Core library
│   ├── AssertBuilders/         # Framework-specific builders
│   ├── ExpressionConverters/   # Expression analysis
│   └── Extensions/             # Helper extensions
├── AssertHelper.Core.Tests/    # Core unit tests
├── AssertHelper.NUnit.Tests/   # NUnit integration tests
├── AssertHelper.MSTest.Tests/  # MSTest integration tests
└── AssertHelper.NuGet/         # NuGet package specs
```

## Documentation

- [Architecture Guide](ARCHITECTURE.md) - Detailed component design and extension points
- [Examples Cookbook](EXAMPLES.md) - Comprehensive usage examples
- [Contributing Guide](CONTRIBUTING.md) - How to contribute to the project

## Limitations

- OrElse (`||`) operator is not supported for splitting assertions (use `&&` for multiple assertions)
- Some complex nested expressions may fall back to `IsTrue` assertions
- Custom assertion methods from test frameworks are not automatically wrapped

## License

MIT License - see LICENSE file for details

## Credits

Created by [Dror Helper](https://github.com/dhelper)

## Support

- **Issues:** [GitHub Issues](https://github.com/dhelper/AssertHelper/issues)
- **Questions:** Open a discussion on GitHub

## Changelog

### Version 1.0.5
- Added nullable type support (thanks to Daniel Rose)
- Improved lambda expression printing
- Enhanced error messages

### Version 1.0.0
- Initial release
- Support for NUnit and MSTest
- Core expression conversion features

---

**Happy Testing! 🎯**
