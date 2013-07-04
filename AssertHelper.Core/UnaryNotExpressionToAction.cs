using System;
using System.Linq.Expressions;

namespace AssertHelper.Core
{
    internal class UnaryNotExpressionToAction : ExpressionTypeToAction<UnaryExpression>
    {
        protected override bool IsValidInternal(UnaryExpression unaryExpression)
        {
            return unaryExpression.NodeType == ExpressionType.Not;
        }

        protected override Expression<Action> GetActionInternal(UnaryExpression typedExpression)
        {
            var operand = typedExpression.Operand;

            return AssertBuilder.GetIsFalseAction(operand);
        }
    }
}