using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionAreNotEqualWithIEnumerable : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreNotEqualWithIEnumerable(Type assertType)
            : base(assertType, "AreNotEqual", new[] { typeof(IEnumerable), typeof(IEnumerable), typeof(string) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            // TODO: fixme!
            return GetExpression(Expression.Convert(left, typeof(IEnumerable)), Expression.Convert(right, typeof(IEnumerable)), lambda.ToConstantExpression());
        }
    }
}