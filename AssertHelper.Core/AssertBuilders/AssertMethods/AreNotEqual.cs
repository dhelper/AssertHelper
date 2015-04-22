using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class AreNotEqual : AssertMethodBase, IBinaryAssertMethod
    {
        public AreNotEqual(Type assertType)
            : base(assertType, "AreNotEqual", new[] { typeof(object), typeof(object), typeof(string) })
        { }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            return GetExpression(Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object)), lambda.ToConstantExpression());
        }
    }
}