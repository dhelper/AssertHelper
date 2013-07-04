using System;
using System.Linq.Expressions;

namespace AssertHelper.Core
{
    internal class BinaryExpressionNotEquals : ExpressionTypeToAction<BinaryExpression>
    {
        protected override bool IsValidInternal(BinaryExpression binaryExpression)
        {
            return binaryExpression.NodeType == ExpressionType.NotEqual;
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression binaryExpression)
        {
            return AssertBuilder.GetAreNotEqualAction(binaryExpression.Left, binaryExpression.Right);
        }
    }
}