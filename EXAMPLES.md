# AssertHelper Examples Cookbook

Comprehensive examples demonstrating all features of AssertHelper.

## Table of Contents

- [Basic Assertions](#basic-assertions)
- [Boolean Assertions](#boolean-assertions)
- [Equality Assertions](#equality-assertions)
- [Null Checks](#null-checks)
- [String Assertions](#string-assertions)
- [Collection Assertions](#collection-assertions)
- [Type Checking](#type-checking)
- [Complex Boolean Logic](#complex-boolean-logic)
- [Multiple Assertions](#multiple-assertions)
- [Custom Error Messages](#custom-error-messages)
- [Advanced Patterns](#advanced-patterns)
- [Common Pitfalls](#common-pitfalls)

## Basic Assertions

### Simple True/False Checks

```csharp
using AssertHelper.Core;
using NUnit.Framework;

[TestFixture]
public class BasicAssertionExamples
{
    [Test]
    public void BooleanValue_IsTrue()
    {
        bool isValid = true;
        
        // Using AssertHelper
        Expect.That(() => isValid);
        
        // Equivalent to: Assert.IsTrue(isValid);
    }
    
    [Test]
    public void Condition_EvaluatesToTrue()
    {
        int value = 10;
        
        // Any boolean expression works
        Expect.That(() => value > 5);
        Expect.That(() => value >= 10);
        Expect.That(() => value < 20);
        
        // Equivalent to:
        // Assert.IsTrue(value > 5);
        // Assert.IsTrue(value >= 10);
        // Assert.IsTrue(value < 20);
    }
    
    [Test]
    public void ExplicitTrueCheck()
    {
        bool flag = ComputeFlag();
        
        Expect.That(() => flag == true);
        
        // Equivalent to: Assert.IsTrue(flag);
    }
}
```

## Boolean Assertions

### True Assertions

```csharp
[TestFixture]
public class TrueAssertionExamples
{
    [Test]
    public void BooleanExpression()
    {
        int age = 25;
        
        // Direct boolean expression
        Expect.That(() => age >= 18);
    }
    
    [Test]
    public void MethodReturnValue()
    {
        string email = "user@example.com";
        
        // Method call returning bool
        Expect.That(() => email.Contains("@"));
    }
    
    [Test]
    public void PropertyValue()
    {
        var user = new User { IsActive = true };
        
        // Property access
        Expect.That(() => user.IsActive);
    }
    
    [Test]
    public void ExplicitComparison()
    {
        bool condition = CheckCondition();
        
        // Explicit comparison to true
        Expect.That(() => condition == true);
    }
}
```

### False Assertions

```csharp
[TestFixture]
public class FalseAssertionExamples
{
    [Test]
    public void NegationOperator()
    {
        bool isError = false;
        
        // Using negation operator
        Expect.That(() => !isError);
        
        // Equivalent to: Assert.IsFalse(isError);
    }
    
    [Test]
    public void ExplicitFalseCheck()
    {
        bool hasErrors = CheckErrors();
        
        // Explicit comparison to false
        Expect.That(() => hasErrors == false);
    }
    
    [Test]
    public void NegatedExpression()
    {
        int count = 0;
        
        // Negating a comparison
        Expect.That(() => !(count > 0));
    }
}
```

## Equality Assertions

### Value Equality

```csharp
[TestFixture]
public class EqualityExamples
{
    [Test]
    public void IntegerEquality()
    {
        int actual = 42;
        int expected = 42;
        
        Expect.That(() => actual == expected);
        
        // Equivalent to: Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void StringEquality()
    {
        string name = "John Doe";
        
        Expect.That(() => name == "John Doe");
    }
    
    [Test]
    public void ObjectEquality()
    {
        var person1 = new Person { Id = 1, Name = "Alice" };
        var person2 = new Person { Id = 1, Name = "Alice" };
        
        // Uses object's Equals() implementation
        Expect.That(() => person1 == person2);
    }
    
    [Test]
    public void NullableEquality()
    {
        int? nullableValue = 10;
        
        Expect.That(() => nullableValue == 10);
    }
    
    [Test]
    public void ComputedValueEquality()
    {
        var calculator = new Calculator();
        
        Expect.That(() => calculator.Add(2, 3) == 5);
    }
}
```

### Inequality Assertions

```csharp
[TestFixture]
public class InequalityExamples
{
    [Test]
    public void NotEqualValues()
    {
        int actual = 42;
        int unwanted = 0;
        
        Expect.That(() => actual != unwanted);
        
        // Equivalent to: Assert.AreNotEqual(unwanted, actual);
    }
    
    [Test]
    public void DifferentStrings()
    {
        string firstName = "John";
        string lastName = "Doe";
        
        Expect.That(() => firstName != lastName);
    }
    
    [Test]
    public void DifferentObjects()
    {
        var obj1 = new MyClass { Value = 1 };
        var obj2 = new MyClass { Value = 2 };
        
        Expect.That(() => obj1 != obj2);
    }
}
```

## Null Checks

### Null Assertions

```csharp
[TestFixture]
public class NullAssertionExamples
{
    [Test]
    public void ValueIsNull()
    {
        object obj = null;
        
        Expect.That(() => obj == null);
        
        // Equivalent to: Assert.IsNull(obj);
    }
    
    [Test]
    public void MethodReturnsNull()
    {
        var result = FindUser("nonexistent");
        
        Expect.That(() => result == null);
    }
    
    [Test]
    public void NullableHasNoValue()
    {
        int? nullableInt = null;
        
        Expect.That(() => nullableInt == null);
    }
}
```

### Not Null Assertions

```csharp
[TestFixture]
public class NotNullAssertionExamples
{
    [Test]
    public void ValueIsNotNull()
    {
        object obj = new object();
        
        Expect.That(() => obj != null);
        
        // Equivalent to: Assert.IsNotNull(obj);
    }
    
    [Test]
    public void PropertyIsNotNull()
    {
        var user = new User { Name = "John" };
        
        Expect.That(() => user.Name != null);
    }
    
    [Test]
    public void NullableHasValue()
    {
        int? nullableInt = 42;
        
        Expect.That(() => nullableInt != null);
    }
    
    [Test]
    public void ChainedNullChecks()
    {
        var order = GetOrder();
        
        // Multiple null checks
        Expect.That(() => order != null && 
                          order.Customer != null && 
                          order.Customer.Address != null);
    }
}
```

## String Assertions

### String Contains

```csharp
[TestFixture]
public class StringContainsExamples
{
    [Test]
    public void ContainsSubstring()
    {
        string message = "Hello, World!";
        
        Expect.That(() => message.Contains("World"));
        
        // Equivalent to: StringAssert.Contains("World", message);
    }
    
    [Test]
    public void EmailContainsAtSign()
    {
        string email = "user@example.com";
        
        Expect.That(() => email.Contains("@"));
    }
    
    [Test]
    public void ContainsVariable()
    {
        string text = "The quick brown fox";
        string searchTerm = "quick";
        
        Expect.That(() => text.Contains(searchTerm));
    }
    
    [Test]
    public void CaseSensitiveContains()
    {
        string message = "Hello";
        
        // This will fail - case sensitive
        // Expect.That(() => message.Contains("hello"));
        
        // Use ToLower() for case-insensitive
        Expect.That(() => message.ToLower().Contains("hello"));
    }
}
```

### String Starts With

```csharp
[TestFixture]
public class StringStartsWithExamples
{
    [Test]
    public void StartsWithPrefix()
    {
        string filename = "document.pdf";
        
        Expect.That(() => filename.StartsWith("document"));
        
        // Equivalent to: StringAssert.StartsWith("document", filename);
    }
    
    [Test]
    public void UrlStartsWithProtocol()
    {
        string url = "https://example.com";
        
        Expect.That(() => url.StartsWith("https://"));
    }
    
    [Test]
    public void StartsWithVariable()
    {
        string path = "/home/user/documents";
        string prefix = "/home";
        
        Expect.That(() => path.StartsWith(prefix));
    }
}
```

### String Ends With

```csharp
[TestFixture]
public class StringEndsWithExamples
{
    [Test]
    public void EndsWithSuffix()
    {
        string filename = "report.xlsx";
        
        Expect.That(() => filename.EndsWith(".xlsx"));
        
        // Equivalent to: StringAssert.EndsWith(".xlsx", filename);
    }
    
    [Test]
    public void EndsWithExtension()
    {
        string path = "C:\\Documents\\file.txt";
        
        Expect.That(() => path.EndsWith(".txt"));
    }
    
    [Test]
    public void EndsWithVariable()
    {
        string sentence = "This is a test";
        string suffix = "test";
        
        Expect.That(() => sentence.EndsWith(suffix));
    }
}
```

## Collection Assertions

### Collection Contains

```csharp
[TestFixture]
public class CollectionContainsExamples
{
    [Test]
    public void ArrayContainsElement()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        
        Expect.That(() => numbers.Contains(3));
        
        // Equivalent to: CollectionAssert.Contains(numbers, 3);
    }
    
    [Test]
    public void ListContainsElement()
    {
        var names = new List<string> { "Alice", "Bob", "Charlie" };
        
        Expect.That(() => names.Contains("Bob"));
    }
    
    [Test]
    public void HashSetContainsElement()
    {
        var tags = new HashSet<string> { "c#", "testing", "dotnet" };
        
        Expect.That(() => tags.Contains("testing"));
    }
    
    [Test]
    public void ContainsVariable()
    {
        var items = new List<int> { 10, 20, 30 };
        int searchValue = 20;
        
        Expect.That(() => items.Contains(searchValue));
    }
    
    [Test]
    public void ContainsObject()
    {
        var users = new List<User>
        {
            new User { Id = 1, Name = "Alice" },
            new User { Id = 2, Name = "Bob" }
        };
        var targetUser = new User { Id = 2, Name = "Bob" };
        
        // Requires proper Equals() implementation
        Expect.That(() => users.Contains(targetUser));
    }
}
```

### Collection Equality

```csharp
[TestFixture]
public class CollectionEqualityExamples
{
    [Test]
    public void ArraysAreEqual()
    {
        int[] actual = { 1, 2, 3 };
        int[] expected = { 1, 2, 3 };
        
        Expect.That(() => actual == expected);
        
        // Equivalent to: CollectionAssert.AreEqual(expected, actual);
    }
    
    [Test]
    public void ListsAreEqual()
    {
        var actualList = new List<string> { "a", "b", "c" };
        var expectedList = new List<string> { "a", "b", "c" };
        
        Expect.That(() => actualList == expectedList);
    }
    
    [Test]
    public void EmptyCollectionsAreEqual()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();
        
        Expect.That(() => list1 == list2);
    }
    
    [Test]
    public void OrderMatters()
    {
        var list1 = new List<int> { 1, 2, 3 };
        var list2 = new List<int> { 3, 2, 1 };
        
        // This will fail - order is different
        // Expect.That(() => list1 == list2);
    }
}
```

### Collection Inequality

```csharp
[TestFixture]
public class CollectionInequalityExamples
{
    [Test]
    public void ArraysAreNotEqual()
    {
        int[] array1 = { 1, 2, 3 };
        int[] array2 = { 4, 5, 6 };
        
        Expect.That(() => array1 != array2);
        
        // Equivalent to: CollectionAssert.AreNotEqual(array2, array1);
    }
    
    [Test]
    public void DifferentLengths()
    {
        var list1 = new List<int> { 1, 2, 3 };
        var list2 = new List<int> { 1, 2 };
        
        Expect.That(() => list1 != list2);
    }
    
    [Test]
    public void DifferentElements()
    {
        var list1 = new List<string> { "a", "b", "c" };
        var list2 = new List<string> { "a", "b", "d" };
        
        Expect.That(() => list1 != list2);
    }
}
```

## Type Checking

### Instance Of Type

```csharp
[TestFixture]
public class TypeCheckingExamples
{
    [Test]
    public void ObjectIsOfType()
    {
        object obj = "Hello";
        
        Expect.That(() => obj is string);
        
        // Equivalent to: Assert.IsInstanceOfType(typeof(string), obj);
    }
    
    [Test]
    public void InheritanceCheck()
    {
        Animal animal = new Dog();
        
        Expect.That(() => animal is Dog);
        Expect.That(() => animal is Animal);
    }
    
    [Test]
    public void InterfaceImplementation()
    {
        IEnumerable<int> collection = new List<int> { 1, 2, 3 };
        
        Expect.That(() => collection is List<int>);
        Expect.That(() => collection is ICollection<int>);
    }
    
    [Test]
    public void NullableType()
    {
        object obj = (int?)42;
        
        Expect.That(() => obj is int?);
    }
    
    [Test]
    public void ValueTypes()
    {
        object number = 42;
        
        Expect.That(() => number is int);
    }
}
```

## Complex Boolean Logic

### Multiple Conditions with AND

```csharp
[TestFixture]
public class ComplexLogicExamples
{
    [Test]
    public void MultipleConditions_AllMustPass()
    {
        var user = new User
        {
            Name = "John Doe",
            Age = 25,
            Email = "john@example.com",
            IsActive = true
        };
        
        // All conditions are checked independently
        Expect.That(() => 
            user.Name != null &&
            user.Age >= 18 &&
            user.Email.Contains("@") &&
            user.IsActive == true);
        
        // If any fail, you'll see ALL failures, not just the first one
    }
    
    [Test]
    public void ValidationRules()
    {
        var order = new Order
        {
            Id = 1001,
            Total = 99.99m,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>()
        };
        
        order.Items.Add(new OrderItem { Price = 99.99m });
        
        Expect.That(() =>
            order.Id > 0 &&
            order.Total > 0 &&
            order.Status == OrderStatus.Pending &&
            order.Items != null &&
            order.Items.Count > 0);
    }
    
    [Test]
    public void NestedObjectValidation()
    {
        var company = new Company
        {
            Name = "Acme Corp",
            Address = new Address
            {
                Street = "123 Main St",
                City = "Springfield",
                ZipCode = "12345"
            }
        };
        
        Expect.That(() =>
            company.Name != null &&
            company.Address != null &&
            company.Address.Street != null &&
            company.Address.City != null &&
            company.Address.ZipCode.Length == 5);
    }
}
```

### Range Checks

```csharp
[TestFixture]
public class RangeCheckExamples
{
    [Test]
    public void ValueInRange()
    {
        int value = 50;
        
        Expect.That(() => value >= 0 && value <= 100);
    }
    
    [Test]
    public void DateInRange()
    {
        DateTime date = DateTime.Now;
        DateTime start = DateTime.Now.AddDays(-7);
        DateTime end = DateTime.Now.AddDays(7);
        
        Expect.That(() => date >= start && date <= end);
    }
    
    [Test]
    public void MultipleRangeChecks()
    {
        var metrics = new Metrics
        {
            Cpu = 45.5,
            Memory = 78.2,
            Disk = 65.0
        };
        
        Expect.That(() =>
            metrics.Cpu >= 0 && metrics.Cpu <= 100 &&
            metrics.Memory >= 0 && metrics.Memory <= 100 &&
            metrics.Disk >= 0 && metrics.Disk <= 100);
    }
}
```

## Multiple Assertions

### Using Expect.All()

```csharp
[TestFixture]
public class MultipleAssertionExamples
{
    [Test]
    public void RunMultipleAssertions()
    {
        var calculator = new Calculator();
        
        // All assertions run even if some fail
        Expect.All(
            () => Assert.AreEqual(5, calculator.Add(2, 3)),
            () => Assert.AreEqual(1, calculator.Subtract(3, 2)),
            () => Assert.AreEqual(6, calculator.Multiply(2, 3)),
            () => Assert.AreEqual(2, calculator.Divide(6, 3))
        );
    }
    
    [Test]
    public void MixedAssertionTypes()
    {
        var user = GetUser();
        
        Expect.All(
            () => Expect.That(() => user != null),
            () => Expect.That(() => user.Name == "John"),
            () => Expect.That(() => user.Email.Contains("@")),
            () => Expect.That(() => user.Age >= 18)
        );
    }
}
```

### Implicit Multiple Assertions

```csharp
[TestFixture]
public class ImplicitMultipleAssertions
{
    [Test]
    public void UsingAndOperator()
    {
        var product = new Product
        {
            Id = 1,
            Name = "Widget",
            Price = 19.99m,
            InStock = true
        };
        
        // Automatically runs as multiple independent assertions
        Expect.That(() =>
            product.Id > 0 &&
            product.Name != null &&
            product.Price > 0 &&
            product.InStock == true);
        
        // If 2 out of 4 fail, you'll see both failures
    }
}
```

## Custom Error Messages

### Leveraging Lambda Expressions

```csharp
[TestFixture]
public class ErrorMessageExamples
{
    [Test]
    public void SelfDocumentingAssertions()
    {
        int actualTotal = CalculateTotal();
        int expectedTotal = 100;
        
        // The lambda expression itself serves as documentation
        Expect.That(() => actualTotal == expectedTotal);
        
        // Error message will show: "actualTotal == expectedTotal" with values
    }
    
    [Test]
    public void DescriptiveVariableNames()
    {
        var processedRecordCount = ProcessRecords();
        var minimumRequiredRecords = 10;
        
        // Clear what's being tested from the expression
        Expect.That(() => processedRecordCount >= minimumRequiredRecords);
    }
    
    [Test]
    public void ComplexConditionsShowClearly()
    {
        var order = GetOrder();
        
        Expect.That(() =>
            order.Total > 0 &&
            order.Items.Count > 0 &&
            order.Customer != null);
        
        // Each failing condition is clearly shown
    }
}
```

## Advanced Patterns

### Testing Exceptions

```csharp
[TestFixture]
public class ExceptionTestingExamples
{
    [Test]
    public void MethodThrowsException()
    {
        var calculator = new Calculator();
        
        // Use framework-specific exception assertions
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
        
        // Then verify state
        Expect.That(() => calculator.LastOperation == null);
    }
    
    [Test]
    public void NoExceptionThrown()
    {
        var calculator = new Calculator();
        
        // Use framework's DoesNotThrow
        Assert.DoesNotThrow(() => calculator.Add(1, 2));
        
        // Verify result
        Expect.That(() => calculator.LastResult == 3);
    }
}
```

### Parameterized Tests

```csharp
[TestFixture]
public class ParameterizedTestExamples
{
    [TestCase(1, 2, 3)]
    [TestCase(0, 0, 0)]
    [TestCase(-1, 1, 0)]
    [TestCase(100, 200, 300)]
    public void Add_VariousInputs_ReturnsExpectedSum(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(a, b);
        
        Expect.That(() => result == expected);
    }
    
    [TestCase("hello", true)]
    [TestCase("", false)]
    [TestCase(null, false)]
    public void IsValid_VariousStrings_ReturnsExpected(string input, bool expected)
    {
        var validator = new Validator();
        var result = validator.IsValid(input);
        
        Expect.That(() => result == expected);
    }
}
```

### Setup and Teardown

```csharp
[TestFixture]
public class SetupTeardownExamples
{
    private Database _database;
    private TestDataGenerator _dataGenerator;
    
    [SetUp]
    public void Setup()
    {
        _database = new Database();
        _database.Connect();
        _dataGenerator = new TestDataGenerator();
    }
    
    [TearDown]
    public void Teardown()
    {
        _database.Disconnect();
        _database.Dispose();
    }
    
    [Test]
    public void DatabaseOperations()
    {
        var user = _dataGenerator.CreateUser();
        _database.Save(user);
        
        var retrieved = _database.GetById(user.Id);
        
        Expect.That(() =>
            retrieved != null &&
            retrieved.Id == user.Id &&
            retrieved.Name == user.Name);
    }
}
```

### Async/Await Patterns

```csharp
[TestFixture]
public class AsyncExamples
{
    [Test]
    public async Task AsyncMethod_ReturnsExpectedValue()
    {
        var service = new DataService();
        
        var result = await service.GetDataAsync();
        
        // Use Expect.That after await
        Expect.That(() => result != null &&
                          result.Count > 0);
    }
    
    [Test]
    public async Task MultipleAsyncCalls()
    {
        var service = new DataService();
        
        var result1 = await service.GetAsync(1);
        var result2 = await service.GetAsync(2);
        
        Expect.That(() =>
            result1 != null &&
            result2 != null &&
            result1.Id != result2.Id);
    }
}
```

## Common Pitfalls

### OR Operator Not Supported

```csharp
[TestFixture]
public class CommonMistakes
{
    [Test]
    public void OrOperator_NotSupported()
    {
        int value = 5;
        
        // DON'T: OR operator (||) is not split into multiple assertions
        // Expect.That(() => value == 5 || value == 10);
        
        // DO: Use framework-specific assertions or split manually
        bool condition = value == 5 || value == 10;
        Expect.That(() => condition);
        
        // OR: Test each possibility separately
        if (value != 5)
        {
            Expect.That(() => value == 10);
        }
    }
    
    [Test]
    public void ComplexExpressions_MayFallbackToIsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        
        // Complex LINQ expressions may not be optimally converted
        Expect.That(() => list.All(x => x > 0));
        // This becomes: Assert.IsTrue(list.All(x => x > 0))
        
        // For better error messages, extract the result
        bool allPositive = list.All(x => x > 0);
        Expect.That(() => allPositive);
    }
    
    [Test]
    public void AvoidSideEffects()
    {
        var counter = new Counter();
        
        // DON'T: Expressions shouldn't have side effects
        // Expect.That(() => counter.Increment() == 1);
        
        // DO: Separate action from assertion
        var result = counter.Increment();
        Expect.That(() => result == 1);
    }
}
```

### Variable Capture

```csharp
[TestFixture]
public class VariableCaptureExamples
{
    [Test]
    public void VariablesAreCapturedCorrectly()
    {
        int expected = 42;
        int actual = GetValue();
        
        // Variables are captured and evaluated at assertion time
        Expect.That(() => actual == expected);
        // Works correctly
    }
    
    [Test]
    public void MutableVariablesInLoops()
    {
        var values = new[] { 1, 2, 3, 4, 5 };
        
        for (int i = 0; i < values.Length; i++)
        {
            var currentIndex = i;  // Capture loop variable
            var currentValue = values[i];
            
            Expect.That(() => currentValue == values[currentIndex]);
        }
    }
}
```

### Reference vs Value Equality

```csharp
[TestFixture]
public class EqualitySemantics
{
    [Test]
    public void ReferenceEquality()
    {
        var obj1 = new MyClass { Value = 10 };
        var obj2 = new MyClass { Value = 10 };
        
        // DON'T expect reference equality for different instances
        // Expect.That(() => obj1 == obj2);
        // This might fail even if values are the same
        
        // DO: Compare specific properties
        Expect.That(() => obj1.Value == obj2.Value);
        
        // OR: Implement Equals() in MyClass
    }
    
    [Test]
    public void ValueTypeEquality()
    {
        var struct1 = new MyStruct { Value = 10 };
        var struct2 = new MyStruct { Value = 10 };
        
        // Value types compare by value (if Equals is properly implemented)
        Expect.That(() => struct1.Equals(struct2));
    }
}
```

## Performance Tips

### Expression Compilation Overhead

```csharp
[TestFixture]
public class PerformanceConsiderations
{
    [Test]
    public void MultipleAssertions_InOneExpression_IsFaster()
    {
        var user = GetUser();
        
        // BETTER: Single expression compilation
        Expect.That(() =>
            user.Name != null &&
            user.Email != null &&
            user.Age >= 18);
        
        // SLOWER: Multiple compilations
        // Expect.That(() => user.Name != null);
        // Expect.That(() => user.Email != null);
        // Expect.That(() => user.Age >= 18);
    }
    
    [Test]
    public void ExtractComplexComputations()
    {
        // DON'T: Complex computation in expression
        // Expect.That(() => ExpensiveComputation() == expected);
        
        // DO: Compute once, then assert
        var result = ExpensiveComputation();
        Expect.That(() => result == expected);
    }
}
```

---

## Summary

AssertHelper provides a natural, expressive way to write assertions that:

1. **Reads like English**: `Expect.That(() => user.Age >= 18)`
2. **Self-documenting**: The expression itself explains what's being tested
3. **Shows all failures**: Multiple conditions are tested independently
4. **Framework-agnostic**: Same syntax works with NUnit or MSTest
5. **Type-safe**: Compile-time checking of your assertions

For more information, see:
- [README.md](README.md) - Getting started guide
- [ARCHITECTURE.md](ARCHITECTURE.md) - Internal design
- [CONTRIBUTING.md](CONTRIBUTING.md) - How to contribute
