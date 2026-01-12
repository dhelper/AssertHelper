using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    /// <summary>
    /// Defines the contract for converting expression trees into assertion action expressions.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface analyze specific types of expressions (binary, unary, method calls, etc.)
    /// and convert them into appropriate assertion method calls for the test framework in use.
    /// Each converter handles a specific pattern of expression (e.g., equality comparisons, string operations).
    /// </remarks>
    internal interface IExpressionTypeToAction
    {
        /// <summary>
        /// Determines whether this converter can handle the specified expression.
        /// </summary>
        /// <param name="expr">The expression to evaluate.</param>
        /// <returns>True if this converter can convert the expression; otherwise, false.</returns>
        bool IsValid(Expression expr);
        
        /// <summary>
        /// Converts the expression into an assertion action that can be compiled and executed.
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>An action expression that performs the appropriate assertion when executed.</returns>
        Expression<Action> GetAction(Expression expression);
    }
}