using System;
using System.Linq.Expressions;

namespace AssertHelper.Core
{
    internal class BinaryExpressionWithConstantRightAction : BinaryExpressionWithConstantBase
    {
        protected override bool IsValidInternal(BinaryExpression typedExpression)
        {
            return base.IsValidInternal(typedExpression) && typedExpression.Right is ConstantExpression;
        }

        protected override Expression<Action> GetActionInternal(BinaryExpression typedExpression)
        {
            return GetActionForConstant((ConstantExpression) typedExpression.Right, typedExpression.Left, typedExpression.NodeType);
        }
    }
}