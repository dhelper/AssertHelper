using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionAreNotEqualWithIEnumerable : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreNotEqualWithIEnumerable(Type assertType)
            : base(assertType, "AreNotEqual", new[] { typeof(IEnumerable), typeof(IEnumerable) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            // TODO: fixme!
            return GetExpression(Expression.Convert(left, typeof(IEnumerable)), Expression.Convert(right, typeof(IEnumerable)));
        }
    }
}