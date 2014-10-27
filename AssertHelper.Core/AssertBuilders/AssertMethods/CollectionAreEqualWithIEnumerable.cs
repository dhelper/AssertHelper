using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionAreEqualWithIEnumerable : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreEqualWithIEnumerable(Type assertType)
            : base(assertType, "AreEqual", new[] { typeof(IEnumerable), typeof(IEnumerable) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right)
        {
            return GetExpression(Expression.Convert(left, typeof(IEnumerable)), Expression.Convert(right, typeof(IEnumerable)));
        }
    }
}