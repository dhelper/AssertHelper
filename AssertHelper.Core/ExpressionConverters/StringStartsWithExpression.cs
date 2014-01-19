using AssertHelper.Core.ExpressionConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AssertHelper.Core
{
    class StringStartsWithExpression : ExpressionTypeToAction<MethodCallExpression>
    {
        protected override bool IsValidInternal(MethodCallExpression typedExpression)
        {
            var method = typedExpression.Method;

            return method.DeclaringType == typeof(string) && method.Name == "StartsWith";
        }

        protected override Expression<Action> GetActionInternal(MethodCallExpression typedExpression)
        {
            return AssertBuilder.GetStringStartsWith(typedExpression.Arguments[0], typedExpression.Object);
        }
    }
}
