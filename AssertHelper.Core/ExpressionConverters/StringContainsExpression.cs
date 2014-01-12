using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    class StringContainsExpression : ExpressionTypeToAction<MethodCallExpression>
    {
        protected override bool IsValidInternal(MethodCallExpression typedExpression)
        {
            var method = typedExpression.Method;

            return method.DeclaringType == typeof (string) && method.Name == "Contains";
        }

        protected override Expression<Action> GetActionInternal(MethodCallExpression typedExpression)
        {
            return AssertBuilder.GetStringContains(typedExpression.Object, typedExpression.Arguments[0]);
        }
    }
}
