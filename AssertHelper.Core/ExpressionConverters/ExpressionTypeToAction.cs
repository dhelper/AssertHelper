using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal abstract class ExpressionTypeToAction<T> : ExpressionTypeToActionBase where T : Expression
    {
        public override bool IsValid(Expression expr)
        {
            return expr is T && IsValidInternal((T) expr);
        }

        protected virtual bool IsValidInternal(T typedExpression)
        {
            return true;
        }

        public override Expression<Action> GetAction(Expression expression)
        {
            var typedExpression = (T) expression;

            return GetActionInternal(typedExpression);
        }

        protected abstract Expression<Action> GetActionInternal(T typedExpression);
    }
}