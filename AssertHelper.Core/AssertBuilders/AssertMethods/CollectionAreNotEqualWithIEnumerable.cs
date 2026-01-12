using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    /// <summary>
    /// Creates collection inequality assertion for IEnumerable types.
    /// Used by test frameworks that support IEnumerable comparison (e.g., NUnit).
    /// </summary>
    internal class CollectionAreNotEqualWithIEnumerable : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreNotEqualWithIEnumerable(Type assertType)
            : base(assertType, "AreNotEqual", new[] { typeof(IEnumerable), typeof(IEnumerable), typeof(string) })
        {
        }

        /// <summary>
        /// Converts both sides to IEnumerable and creates the assertion expression.
        /// Note: Parameter order matches expected vs actual convention (expected, actual, message).
        /// </summary>
        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            // Convert both expressions to IEnumerable for the assert method
            // The order is: expected (left), actual (right), message (lambda)
            return GetExpression(
                Expression.Convert(left, typeof(IEnumerable)), 
                Expression.Convert(right, typeof(IEnumerable)), 
                lambda.ToConstantExpression());
        }
    }
}