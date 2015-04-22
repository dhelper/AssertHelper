using System;
using System.Linq;
using System.Linq.Expressions;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class BinaryExpressionEquals : ExpressionTypeToAction<BinaryExpression>
    {
        protected override bool IsValidInternal(BinaryExpression binaryExpression)
        {
            return binaryExpression.NodeType == ExpressionType.Equal;
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression binaryExpression, string lambda)
        {
            if (binaryExpression.Left.Type.IsNot<string>() && binaryExpression.Left.Type.IsEnumerableType())
            {
                return AssertBuilder.GetCollectionEquals(binaryExpression.Right, binaryExpression.Left, lambda);
            }

            return AssertBuilder.GetAreEqualAction(binaryExpression.Right, binaryExpression.Left, lambda);
        }
    }
}