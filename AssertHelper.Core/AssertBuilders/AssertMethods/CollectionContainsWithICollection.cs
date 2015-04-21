using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionContainsWithICollection : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionContainsWithICollection(Type assertType)
            : base(assertType, "Contains", new[] { typeof(ICollection), typeof(object) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            // TODO: fixMe!
            return GetExpression(Expression.Convert(left, typeof(ICollection)), Expression.Convert(right, typeof(object)));
        }
    }
}