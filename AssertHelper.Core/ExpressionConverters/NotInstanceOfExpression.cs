using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class NotInstanceOfExpression : ExpressionTypeToAction<TypeBinaryExpression>
    {
        protected override Expression<Action> GetActionInternal(TypeBinaryExpression typedExpression)
        {
            return AssertBuilder.GetIsInstanceOf(typedExpression.TypeOperand, typedExpression.Expression);
        }
    }
}