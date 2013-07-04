using System;
using System.Linq.Expressions;

namespace AssertHelper.Core
{
    internal abstract class BinaryExpressionWithConstantBase : ExpressionTypeToAction<BinaryExpression>
    {
        protected override bool IsValidInternal(BinaryExpression typedExpression)
        {
            return typedExpression.NodeType == ExpressionType.Equal || typedExpression.NodeType == ExpressionType.NotEqual;
        }

        protected Expression<Action> GetActionForConstant(ConstantExpression constantExpression, Expression resultExpression, ExpressionType expressionType)
        {
            var value = (bool) constantExpression.Value;
            if (expressionType == ExpressionType.NotEqual)
            {
                value = !value;
            }

            return value ? AssertBuilder.GetIsTrueAction(resultExpression) : AssertBuilder.GetIsFalseAction(resultExpression);
        }
    }
}