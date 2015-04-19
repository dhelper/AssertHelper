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

        protected override Expression<Action> GetActionInternal(BinaryExpression typedExpression)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression binaryExpression, string lambdaString)
        {
            if (binaryExpression.Left.Type.IsNot<string>() && binaryExpression.Left.Type.IsEnumerableType())
            {
                return AssertBuilder.GetCollectionEquals(binaryExpression.Right, binaryExpression.Left);
            }

            return AssertBuilder.GetAreEqualAction(binaryExpression.Right, binaryExpression.Left);
        }

        public override string GetLembda(BinaryExpression binaryExpression)
        {
            var left = binaryExpression.Left.ToString();
            var leftShort = left.Split('.').LastOrDefault();

            var right = binaryExpression.Right.ToString();
            var rightShort = right.Split('.').LastOrDefault();

            return binaryExpression.ToString().Replace(left, leftShort).Replace(right, rightShort);
        }
    }
}