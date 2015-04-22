using System;
using System.Linq.Expressions;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class BinaryExpressionNotEquals : ExpressionTypeToAction<BinaryExpression>
    {
        protected override bool IsValidInternal(BinaryExpression binaryExpression)
        {
            return binaryExpression.NodeType == ExpressionType.NotEqual;
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression binaryExpression, string lambda)
        {
            if (binaryExpression.Left.Type.IsNot<string>() && binaryExpression.Left.Type.IsEnumerableType())
            {
                return AssertBuilder.GetCollectionNotEquals(binaryExpression.Right, binaryExpression.Left, lambda);
            }

            return AssertBuilder.GetAreNotEqualAction(binaryExpression.Right, binaryExpression.Left, lambda);
        }
    }
}