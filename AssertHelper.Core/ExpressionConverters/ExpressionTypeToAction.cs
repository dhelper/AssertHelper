using System;
using System.Linq.Expressions;

namespace AssertHelper.Core
{
    internal abstract class ExpressionTypeToActionBase : IExpressionTypeToAction
    {
        private readonly AssertBuilder _assertBuilder = new AssertBuilder();
        protected AssertBuilder AssertBuilder
        {
            get { return _assertBuilder; }
        }

        public abstract bool IsValid(Expression expr);
        public abstract Expression<Action> GetAction(Expression expression);
    }

    internal abstract class ExpressionTypeToAction<T> : ExpressionTypeToActionBase where T : Expression 
    {
        public override bool IsValid(Expression expr)
        {
            return expr is T && IsValidInternal((T)expr);
        }

        protected virtual bool IsValidInternal(T typedExpression)
        {
            return true;
        }

        public override Expression<Action> GetAction(Expression expression)
        {
            var typedExpression = (T)expression;

            return GetActionInternal(typedExpression);
        }

        protected abstract Expression<Action> GetActionInternal(T typedExpression);
    }
}