using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class NotInstanceOf : ExpressionTypeToAction<TypeBinaryExpression>
    {
        protected override Expression<Action> GetActionInternal(TypeBinaryExpression typedExpression, string lambda)
        {
            return AssertBuilder.GetIsInstanceOf(typedExpression.TypeOperand, typedExpression.Expression, lambda);
        }
    }
}