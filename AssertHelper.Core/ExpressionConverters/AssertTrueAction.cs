﻿using System;
using System.Linq.Expressions;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.ExpressionConverters
{
    internal class AssertTrueAction : ExpressionTypeToActionBase
    {
        public override bool IsValid(Expression expr)
        {
            return true;
        }

        public override Expression<Action> GetAction(Expression expression)
        {
            return AssertBuilder.GetIsTrueAction(expression, expression.PrettyPrint());
        }
    }
}