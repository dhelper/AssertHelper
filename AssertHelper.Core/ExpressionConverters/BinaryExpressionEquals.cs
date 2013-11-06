using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class BinaryExpressionEquals : ExpressionTypeToAction<BinaryExpression>
    {
        protected override bool IsValidInternal(BinaryExpression binaryExpression)
        {
            return binaryExpression.NodeType == ExpressionType.Equal;
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression binaryExpression)
        {
            return AssertBuilder.GetAreEqualAction(binaryExpression.Left, binaryExpression.Right);
        }
    }
}