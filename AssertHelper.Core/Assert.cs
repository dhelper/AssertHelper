using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace AssertHelper.Core
{
    public static class Assert
    {
        private static readonly List<IExpressionTypeToAction> _actionConverters;
        private static readonly IExpressionTypeToAction _defaultActionConverter = new AssertTrueAction();
        static Assert()
        {
            _actionConverters = new List<IExpressionTypeToAction>
            {
                new UnaryNotExpressionToAction(),
                new BinaryExpressionWithConstantRightAction(),
                new BinaryExpressionWithConstantLeftAction(),
                new BinaryExpressionEquals(),
                new BinaryExpressionNotEquals(),
                new InstanceOfExpression()
            };
        }

        public static void This(Expression<Func<bool>> predicate)
        {
            var expression = predicate.Body;

            var actionConverter = _actionConverters.FirstOrDefault(action => action.IsValid(expression));
            if (actionConverter == null)
            {
                actionConverter = _defaultActionConverter;
            }

            var assert = actionConverter.GetAction(expression);

            assert.Compile().Invoke();
        }
    }

    internal class InstanceOfExpression : ExpressionTypeToAction<TypeBinaryExpression>
    {
        protected override bool IsValidInternal(TypeBinaryExpression typedExpression)
        {
            return typedExpression.NodeType == ExpressionType.TypeIs;
        }

        protected override Expression<Action> GetActionInternal(TypeBinaryExpression typedExpression)
        {
            return AssertBuilder.GetIsInstanceOf(typedExpression.TypeOperand, typedExpression.Expression);
        }
    }

    internal class NotInstanceOfExpression : ExpressionTypeToAction<TypeBinaryExpression>
    {
        protected override Expression<Action> GetActionInternal(TypeBinaryExpression typedExpression)
        {
            return AssertBuilder.GetIsInstanceOf(typedExpression.TypeOperand, typedExpression.Expression);
        }
    }
}
