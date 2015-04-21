using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionAreNotEqualWithICollection : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreNotEqualWithICollection(Type assertType)
            : base(assertType, "AreNotEqual", new[] { typeof(ICollection), typeof(ICollection) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            // TODO: fixme!
            return GetExpression(Expression.Convert(left, typeof(ICollection)), Expression.Convert(right, typeof(ICollection)));
        }
    }
}