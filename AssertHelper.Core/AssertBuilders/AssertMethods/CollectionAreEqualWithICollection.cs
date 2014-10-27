using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionAreEqualWithICollection : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreEqualWithICollection(Type assertType)
            : base(assertType, "AreEqual", new[] { typeof(ICollection), typeof(ICollection) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right)
        {
            return GetExpression(Expression.Convert(left, typeof(ICollection)), Expression.Convert(right, typeof(ICollection)));
        }
    }
}