using AssertHelper.Core.AssertBuilders;
using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.ExpressionConverters
{
    public class EnumerableContains : IExpressionTypeToAction
    {
        public bool IsValid(Expression expr)
        {
            if (expr.NodeType != ExpressionType.Call)
            {
                return false;
            }

            var methodCallExpr = (MethodCallExpression)expr;

            var declaringType = methodCallExpr.Method.DeclaringType;
            if (declaringType == null)
            {
                return false;
            }

            if (declaringType.Name == "Enumerable" && methodCallExpr.Method.Name == "Contains")
            {
                return true;
            }

            return false;
        }

        public Expression<Action> GetAction(Expression expression)
        {
            var methodCallExpr = (MethodCallExpression)expression;

            return AssertBuilderFactory.GetAssertBuilder().GetCollectionContains(methodCallExpr.Arguments[0], methodCallExpr.Arguments[1], string.Empty);
        }
    }
}
