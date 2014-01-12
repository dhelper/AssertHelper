using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders;

namespace AssertHelper.Core.ExpressionConverters
{
    internal abstract class ExpressionTypeToActionBase : IExpressionTypeToAction
    {
        private readonly IAssertBuilder _assertBuilder = AssertBuilderFaxctory.GetAssertBuilder();

        protected IAssertBuilder AssertBuilder
        {
            get { return _assertBuilder; }
        }

        public abstract bool IsValid(Expression expr);
        public abstract Expression<Action> GetAction(Expression expression);
    }
}