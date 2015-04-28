using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class IsNull : AssertMethodBase, IUnaryAssertMethod
    {
        public IsNull(Type assertType)
            : base(assertType, "IsNull", new[] { typeof(object), typeof(string) })
        { }

        public Expression<Action> Assert(Expression expression, string lambda)
        {
            return GetExpression(Expression.Convert(expression, typeof(object)), lambda.ToConstantExpression());
        }
    }
}