using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AssertHelper.Core.AssertBuilders;
using AssertHelper.Core.ExpressionConverters;

namespace AssertHelper.Core
{
    /// <summary>
    /// Provides a unified assertion API that converts lambda expressions into appropriate test framework assertions.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Expect class is the main entry point for AssertHelper. It allows you to write assertions using 
    /// natural C# expressions that are automatically converted to the appropriate assertions for your test framework.
    /// </para>
    /// <para>
    /// Supported test frameworks: NUnit, MSTest
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Simple equality assertion
    /// Expect.That(() => actual == expected);
    /// 
    /// // Multiple assertions run independently
    /// Expect.That(() => value == 100 &amp;&amp; name != null &amp;&amp; result.IsValid);
    /// 
    /// // String assertions
    /// Expect.That(() => message.Contains("error"));
    /// Expect.That(() => text.StartsWith("Hello"));
    /// 
    /// // Collection assertions
    /// Expect.That(() => list.Contains(item));
    /// Expect.That(() => actual == expected); // For collections
    /// 
    /// // Type checking
    /// Expect.That(() => obj is ExpectedType);
    /// 
    /// // Boolean assertions
    /// Expect.That(() => condition == true);
    /// Expect.That(() => flag == false);
    /// Expect.That(() => !negated);
    /// 
    /// // Null checking
    /// Expect.That(() => obj == null);
    /// Expect.That(() => obj != null);
    /// </code>
    /// </example>
    public static class Expect
    {
        private static readonly List<IExpressionTypeToAction> _actionConverters;
        private static readonly IExpressionTypeToAction _defaultActionConverter = new AssertTrueAction();
        
        static Expect()
        {
            _actionConverters = new List<IExpressionTypeToAction>
            {
                new StringContains(),
                new StringStartsWith(),
                new StringEndsWith(),
                new UnaryNotExpressionToAction(),
                new BinaryExpressionWithConstantRightAction(),
                new BinaryExpressionWithConstantLeftAction(),
                new EnumerableEquals(),
                new BinaryExpressionEquals(),
                new BinaryExpressionNotEquals(),
                new IsInstanceOf(),
                new EnumerableContains(),
                new CollectionContains()                
            };
        }

        /// <summary>
        /// Executes an assertion based on the provided boolean expression.
        /// </summary>
        /// <param name="predicate">A lambda expression representing the assertion condition. The expression is analyzed and converted to the appropriate assertion method.</param>
        /// <remarks>
        /// <para>
        /// When the expression contains the AndAlso (&amp;&amp;) operator, each sub-expression is evaluated independently.
        /// All assertions are executed even if some fail, and all failures are reported together.
        /// </para>
        /// <para>
        /// The method analyzes the expression tree to determine the most appropriate assertion:
        /// <list type="bullet">
        /// <item><description>Equality comparisons (==) → AreEqual</description></item>
        /// <item><description>Inequality comparisons (!=) → AreNotEqual</description></item>
        /// <item><description>Null checks (== null, != null) → IsNull, IsNotNull</description></item>
        /// <item><description>Boolean constants (== true, == false) → IsTrue, IsFalse</description></item>
        /// <item><description>Type checks (is) → IsInstanceOfType</description></item>
        /// <item><description>String operations (Contains, StartsWith, EndsWith) → StringAssert methods</description></item>
        /// <item><description>Collection operations → CollectionAssert methods</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <exception cref="AssertionException">Thrown when the assertion fails (specific exception type depends on the test framework).</exception>
        /// <example>
        /// <code>
        /// // Single assertion
        /// Expect.That(() => result == 42);
        /// 
        /// // Multiple assertions - all executed independently
        /// Expect.That(() => user.Name != null &amp;&amp; user.Age > 0 &amp;&amp; user.Email.Contains("@"));
        /// </code>
        /// </example>
        public static void That(Expression<Func<bool>> predicate)
        {
            var expression = predicate.Body;

            IEnumerable<Expression<Action>> assertExpressions = GetAllAssertExpressions(expression).ToArray();
            if (assertExpressions.Count() > 1)
            {
                var actions = assertExpressions.Select(expression1 => expression1.Compile());
                All(actions.ToArray());
            }
            else
            {
                assertExpressions.Single().Compile().Invoke();
            }
        }

        /// <summary>
        /// Executes multiple assertion actions independently and reports all failures.
        /// </summary>
        /// <param name="assertionsToRun">An array of assertion actions to execute.</param>
        /// <remarks>
        /// <para>
        /// This method is useful when you want to run multiple assertions and see all failures at once,
        /// rather than stopping at the first failure. Each assertion is executed independently, and
        /// all exceptions are collected and reported together.
        /// </para>
        /// <para>
        /// The error message will include the count of failed assertions and detailed information about each failure.
        /// </para>
        /// </remarks>
        /// <exception cref="AssertionException">Thrown when one or more assertions fail, with a summary of all failures.</exception>
        /// <example>
        /// <code>
        /// Expect.All(
        ///     () => Assert.AreEqual(expected1, actual1),
        ///     () => Assert.AreEqual(expected2, actual2),
        ///     () => Assert.IsTrue(condition)
        /// );
        /// </code>
        /// </example>
        public static void All(params Action[] assertionsToRun)
        {
            var errorMessages = new List<Exception>();
            foreach (var action in assertionsToRun)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception exc)
                {
                    errorMessages.Add(exc);
                }
            }

            if (errorMessages.Count <= 0)
                return;

            var errorText = new StringBuilder();
            foreach (var e in errorMessages)
            {
                if (errorText.Length > 0)
                {
                    errorText.Append(Environment.NewLine);
                }

                errorText.Append(DigestStackTrace(e));
            }

            var assertBuilder = AssertBuilderFactory.GetAssertBuilder();

            var errorMessage = string.Format("{0}/{1} conditions failed:{2}{2}{3}", errorMessages.Count, assertionsToRun.Length, Environment.NewLine, errorText);

            assertBuilder.GetFail(errorMessage).Compile().Invoke();
        }

        /// <summary>
        /// Cleans up exception stack traces to remove framework internals and provide clearer error messages.
        /// </summary>
        /// <param name="e">The exception to process.</param>
        /// <returns>A formatted string containing the exception message and cleaned stack trace.</returns>
        private static string DigestStackTrace(Exception e)
        {
            if (e == null)
                return string.Empty;

            var result = new StringBuilder();
            result.AppendLine(e.Message);

            if (!string.IsNullOrEmpty(e.StackTrace))
            {
                var stackTrace = e.StackTrace;
                var lines = stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                
                // Filter out framework internal stack frames
                var filteredLines = lines.Where(line => 
                    !line.Contains("AssertHelper.Core.Expect") &&
                    !line.Contains("System.Linq.Expressions.") &&
                    !line.Contains("System.Reflection.") &&
                    !line.Contains("lambda_method") &&
                    !Regex.IsMatch(line, @"at\s+lambda_method")).ToList();

                if (filteredLines.Any())
                {
                    result.AppendLine("Stack Trace:");
                    foreach (var line in filteredLines.Take(3)) // Limit to top 3 frames for clarity
                    {
                        result.AppendLine("  " + line.Trim());
                    }
                }
            }

            return result.ToString().TrimEnd();
        }


        /// <summary>
        /// Recursively extracts all assertion expressions from a complex expression tree.
        /// </summary>
        /// <param name="expression">The expression to analyze.</param>
        /// <returns>An enumerable of assertion action expressions.</returns>
        /// <remarks>
        /// When the expression contains AndAlso operators, it is split into individual assertions.
        /// Otherwise, a single assertion is returned.
        /// </remarks>
        private static IEnumerable<Expression<Action>> GetAllAssertExpressions(Expression expression)
        {
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                var logicalBinaryExpression = (BinaryExpression)expression;
                var rightSide = GetAllAssertExpressions(logicalBinaryExpression.Right);
                var leftSide = GetAllAssertExpressions(logicalBinaryExpression.Left);

                return leftSide.Concat(rightSide);
            }

            return new[] { GetAssertExpression(expression) };
        }

        /// <summary>
        /// Converts an expression into the appropriate assertion action.
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>An action expression representing the assertion.</returns>
        /// <remarks>
        /// The method iterates through registered expression converters to find the first one that can handle
        /// the given expression. If no specific converter is found, the default IsTrue assertion is used.
        /// </remarks>
        private static Expression<Action> GetAssertExpression(Expression expression)
        {
            var converter = _actionConverters.FirstOrDefault(action => action.IsValid(expression));
            var actionConverter = converter ?? _defaultActionConverter;

            return actionConverter.GetAction(expression);
        }
    }
}
