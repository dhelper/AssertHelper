using System;
using System.Collections;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class CollectionAreNotEqualWithICollection : AssertMethodBase, IBinaryAssertMethod
    {
        public CollectionAreNotEqualWithICollection(Type assertType)
            : base(assertType, "AreNotEqual", new[] { typeof(ICollection), typeof(ICollection), typeof(string) })
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            return GetExpression(Expression.Convert(left, typeof(ICollection)), Expression.Convert(right, typeof(ICollection)), lambda.ToConstantExpression());
        }
    }
}