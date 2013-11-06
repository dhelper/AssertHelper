using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class BinaryExpressionWithConstantLeftAction : BinaryExpressionWithConstantBase
    {
        protected override bool IsValidInternal(BinaryExpression typedExpression)
        {
            return base.IsValidInternal(typedExpression) && typedExpression.Left is ConstantExpression;
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression typedExpression)
        {
            return GetActionForConstant((ConstantExpression)typedExpression.Left, typedExpression.Right, typedExpression.NodeType);
        }
    }
}