using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class IsNotNull : AssertMethodBase, IUnaryAssertMethod
    {
        public IsNotNull(Type assertType)
            : base(assertType, "IsNotNull", new[] { typeof(object), typeof(string) })
        { }

        public Expression<Action> Assert(Expression expression, string lambda)
        {
            return GetExpression(Expression.Convert(expression, typeof(object)), lambda.ToConstantExpression());
        }
    }
}