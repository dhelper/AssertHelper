﻿using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    class StringStartsWith : ExpressionTypeToAction<MethodCallExpression>
    {
        protected override bool IsValidInternal(MethodCallExpression typedExpression)
        {
            var method = typedExpression.Method;

            return method.DeclaringType == typeof(string) && method.Name == "StartsWith";
        }

        protected override Expression<Action> GetActionInternal(MethodCallExpression typedExpression)
        {
            return AssertBuilder.GetStringStartsWith(typedExpression.Arguments[0], typedExpression.Object, string.Empty);
        }
    }
}
