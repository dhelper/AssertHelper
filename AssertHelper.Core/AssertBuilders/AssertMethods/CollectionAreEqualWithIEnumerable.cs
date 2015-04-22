using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionAreEqualWithIEnumerable : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreEqualWithIEnumerable(Type assertType)
            : base(assertType, "AreEqual", new[] { typeof(IEnumerable), typeof(IEnumerable), typeof(string) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            return GetExpression(Expression.Convert(left, typeof(IEnumerable)), Expression.Convert(right, typeof(IEnumerable)), lambda.ToConstantExpression());
        }
    }
}