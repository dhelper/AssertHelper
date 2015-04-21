using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders;

namespace AssertHelper.Core.ExpressionConverters
{
    public class CollectionContains : IExpressionTypeToAction
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

            if (declaringType.FullName.StartsWith("System.Collections.Generic") && methodCallExpr.Method.Name == "Contains")
            {
                return true;
            }

            return false;
        }

        public Expression<Action> GetAction(Expression expression)
        {
            var methodCallExpr = (MethodCallExpression)expression;

            return AssertBuilderFactory.GetAssertBuilder().GetCollectionContains(methodCallExpr.Object, methodCallExpr.Arguments[0], string.Empty);
        }
    }
}