using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal abstract class BinaryExpressionWithConstantBase : ExpressionTypeToAction<BinaryExpression>
    {
        protected abstract Func<BinaryExpression, Expression> GetConstantExpression { get; }
        protected abstract Func<BinaryExpression, Expression> GetValueExpression { get; }

        protected override bool IsValidInternal(BinaryExpression typedExpression)
        {
            var expression = GetConstantExpression(typedExpression);

            var constantExpression = expression as ConstantExpression;
            if (constantExpression == null)
            {
                return false;
            }

            return IsValidConstantExpression(constantExpression) && (
                typedExpression.NodeType == ExpressionType.Equal ||
                typedExpression.NodeType == ExpressionType.NotEqual);
        }

        private bool IsValidConstantExpression(ConstantExpression constantExpression)
        {
            return constantExpression.Value == null || constantExpression.Value is bool;
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression typedExpression)
        {
            var constantExpression = (ConstantExpression)GetConstantExpression(typedExpression);
            var valueExpression = GetValueExpression(typedExpression);

            return GetActionForConstant(constantExpression, valueExpression, typedExpression.NodeType);
        }

        private Expression<Action> GetActionForConstant(ConstantExpression constantExpression, Expression resultExpression, ExpressionType expressionType)
        {
            if (constantExpression.Value == null)
            {
                if (expressionType == ExpressionType.Equal)
                {
                    return AssertBuilder.GetIsNullAction(resultExpression);
                }
                else
                {
                    return AssertBuilder.GetIsNotNullAction(resultExpression);
                }
            }

            var value = (bool)constantExpression.Value;
            if (expressionType == ExpressionType.NotEqual)
            {
                value = !value;
            }

            return value
                       ? AssertBuilder.GetIsTrueAction(resultExpression)
                       : AssertBuilder.GetIsFalseAction(resultExpression);
        }
    }
}