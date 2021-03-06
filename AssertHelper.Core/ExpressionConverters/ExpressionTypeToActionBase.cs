﻿using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders;

namespace AssertHelper.Core.ExpressionConverters
{
    internal abstract class ExpressionTypeToActionBase : IExpressionTypeToAction
    {
        protected IAssertBuilder AssertBuilder
        {
            get { return AssertBuilderFactory.GetAssertBuilder(); }
        }

        public abstract bool IsValid(Expression expr);
        public abstract Expression<Action> GetAction(Expression expression);
    }
}