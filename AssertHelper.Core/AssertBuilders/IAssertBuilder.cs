using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.AssertBuilders
{
    /// <summary>
    /// Defines the contract for building test framework-specific assertion expressions.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface abstracts the creation of assertion expressions for different testing frameworks.
    /// Implementations exist for NUnit and MSTest, converting generic assertion requests into
    /// framework-specific assertion calls.
    /// </para>
    /// <para>
    /// All methods return <see cref="Expression{Action}"/> objects that can be compiled and executed,
    /// allowing for deferred execution and easier testing of the assertion building logic itself.
    /// </para>
    /// <para>
    /// The lambda parameter in most methods represents the original expression string for better error messages.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Example implementation pattern
    /// public class MyTestAssertBuilder : IAssertBuilder
    /// {
    ///     public Expression&lt;Action&gt; GetAreEqualAction(Expression left, Expression right, string lambda)
    ///     {
    ///         // Convert to: () => MyTestFramework.AreEqual(left, right, lambda)
    ///         return Expression.Lambda&lt;Action&gt;(
    ///             Expression.Call(
    ///                 typeof(MyTestFramework).GetMethod("AreEqual"),
    ///                 left, right, Expression.Constant(lambda)));
    ///     }
    ///     // ... other methods
    /// }
    /// </code>
    /// </example>
    public interface IAssertBuilder
    {
        /// <summary>
        /// Creates an equality assertion expression.
        /// </summary>
        /// <param name="left">The expected value expression.</param>
        /// <param name="right">The actual value expression.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that performs the equality assertion when executed.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => actual == expected)</c> to framework-specific equality assertion.
        /// </example>
        Expression<Action> GetAreEqualAction(Expression left, Expression right, string lambda);

        /// <summary>
        /// Creates an inequality assertion expression.
        /// </summary>
        /// <param name="left">The first value expression.</param>
        /// <param name="right">The second value expression.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that performs the inequality assertion when executed.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => actual != unwanted)</c> to framework-specific inequality assertion.
        /// </example>
        Expression<Action> GetAreNotEqualAction(Expression left, Expression right, string lambda);

        /// <summary>
        /// Creates a "true" assertion expression.
        /// </summary>
        /// <param name="expression">The boolean expression to evaluate.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the expression evaluates to true.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => condition)</c> or <c>Expect.That(() => value == true)</c> to IsTrue assertion.
        /// </example>
        Expression<Action> GetIsTrueAction(Expression expression, string lambda);

        /// <summary>
        /// Creates a "false" assertion expression.
        /// </summary>
        /// <param name="expression">The boolean expression to evaluate.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the expression evaluates to false.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => value == false)</c> or <c>Expect.That(() => !condition)</c> to IsFalse assertion.
        /// </example>
        Expression<Action> GetIsFalseAction(Expression expression, string lambda);

        /// <summary>
        /// Creates a type checking assertion expression.
        /// </summary>
        /// <param name="typeOperand">The expected type.</param>
        /// <param name="expression">The expression to type-check.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the expression is an instance of the specified type.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => obj is ExpectedType)</c> to IsInstanceOfType assertion.
        /// </example>
        Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression, string lambda);

        /// <summary>
        /// Creates a failure assertion with a custom message.
        /// </summary>
        /// <param name="message">The failure message to display.</param>
        /// <returns>An action expression that fails the test with the specified message.</returns>
        /// <remarks>
        /// This is typically used internally when multiple assertions fail, to report all failures together.
        /// </remarks>
        Expression<Action> GetFail(string message);

        /// <summary>
        /// Creates a collection containment assertion expression.
        /// </summary>
        /// <param name="expected">The item expected to be in the collection.</param>
        /// <param name="actual">The collection expression.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the collection contains the specified item.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => collection.Contains(item))</c> to CollectionAssert.Contains.
        /// </example>
        Expression<Action> GetCollectionContains(Expression expected, Expression actual, string lambda);

        /// <summary>
        /// Creates a collection equality assertion expression.
        /// </summary>
        /// <param name="expected">The expected collection expression.</param>
        /// <param name="actual">The actual collection expression.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the collections are equal (same items in same order).</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => actualList == expectedList)</c> to CollectionAssert.AreEqual for collections.
        /// </example>
        Expression<Action> GetCollectionEquals(Expression expected, Expression actual, string lambda);

        /// <summary>
        /// Creates a collection inequality assertion expression.
        /// </summary>
        /// <param name="expected">The first collection expression.</param>
        /// <param name="actual">The second collection expression.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the collections are not equal.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => actualList != otherList)</c> to CollectionAssert.AreNotEqual for collections.
        /// </example>
        Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual, string lambda);

        /// <summary>
        /// Creates a string containment assertion expression.
        /// </summary>
        /// <param name="expected">The substring expected to be found.</param>
        /// <param name="actual">The string expression to search in.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the string contains the specified substring.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => text.Contains("substring"))</c> to StringAssert.Contains.
        /// </example>
        Expression<Action> GetStringContains(Expression expected, Expression actual, string lambda);

        /// <summary>
        /// Creates a string "starts with" assertion expression.
        /// </summary>
        /// <param name="expected">The prefix expected at the start of the string.</param>
        /// <param name="actual">The string expression to check.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the string starts with the specified prefix.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => text.StartsWith("prefix"))</c> to StringAssert.StartsWith.
        /// </example>
        Expression<Action> GetStringStartsWith(Expression expected, Expression actual, string lambda);

        /// <summary>
        /// Creates a string "ends with" assertion expression.
        /// </summary>
        /// <param name="expected">The suffix expected at the end of the string.</param>
        /// <param name="actual">The string expression to check.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the string ends with the specified suffix.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => text.EndsWith("suffix"))</c> to StringAssert.EndsWith.
        /// </example>
        Expression<Action> GetStringEndsWith(Expression expected, Expression actual, string lambda);

        /// <summary>
        /// Creates a null assertion expression.
        /// </summary>
        /// <param name="expression">The expression to check for null.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the expression is null.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => obj == null)</c> to Assert.IsNull.
        /// </example>
        Expression<Action> GetIsNullAction(Expression expression, string lambda);

        /// <summary>
        /// Creates a not-null assertion expression.
        /// </summary>
        /// <param name="expression">The expression to check for non-null.</param>
        /// <param name="lambda">The string representation of the original lambda expression for error messages.</param>
        /// <returns>An action expression that asserts the expression is not null.</returns>
        /// <example>
        /// Converts: <c>Expect.That(() => obj != null)</c> to Assert.IsNotNull.
        /// </example>
        Expression<Action> GetIsNotNullAction(Expression expression, string lambda);
    }
}