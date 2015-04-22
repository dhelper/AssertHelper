using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    class StringContains : ExpressionTypeToAction<MethodCallExpression>
    {
        protected override bool IsValidInternal(MethodCallExpression typedExpression)
        {
            var method = typedExpression.Method;

            return method.DeclaringType == typeof (string) && method.Name == "Contains";
        }

        protected override Expression<Action> GetActionInternal(MethodCallExpression typedExpression, string lambda)
        {
            return AssertBuilder.GetStringContains(typedExpression.Arguments[0], typedExpression.Object, lambda);
        }
    }
}
