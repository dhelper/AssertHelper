using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssertHelper.Core.ExpressionConverters
{
    class StringEndsWithExpression : ExpressionTypeToAction<MethodCallExpression>
    {
        protected override bool IsValidInternal(MethodCallExpression typedExpression)
        {
            var method = typedExpression.Method;

            return method.DeclaringType == typeof(string) && method.Name == "EndsWith";
        }

        protected override Expression<Action> GetActionInternal(MethodCallExpression typedExpression)
        {
            return AssertBuilder.GetStringEndsWith(typedExpression.Object, typedExpression.Arguments[0]);
        }
    }
}
