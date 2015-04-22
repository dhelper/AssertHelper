using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionContainsWithIEnumerable : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionContainsWithIEnumerable(Type assertType)
            : base(assertType, "Contains", new[] { typeof(IEnumerable), typeof(object), typeof(string) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            return GetExpression(Expression.Convert(left, typeof(IEnumerable)), Expression.Convert(right, typeof(object)), lambda.ToConstantExpression());
        }
    }
}