using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class InstanceOfExpression : ExpressionTypeToAction<TypeBinaryExpression>
    {
        protected override bool IsValidInternal(TypeBinaryExpression typedExpression)
        {
            return typedExpression.NodeType == ExpressionType.TypeIs;
        }

        protected override Expression<Action> GetActionInternal(TypeBinaryExpression typedExpression)
        {
            return AssertBuilder.GetIsInstanceOf(typedExpression.TypeOperand, typedExpression.Expression);
        }
    }
}