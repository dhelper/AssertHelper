using AssertHelper.Core.AssertBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssertHelper.Core.ExpressionConverters
{
    public class EnumerableContains : IExpressionTypeToAction
    {
        public bool IsValid(System.Linq.Expressions.Expression expr)
        {
            if (expr.NodeType != ExpressionType.Call)
            {
                return false;
            }

            var methodCallExpr = (MethodCallExpression)expr;

            if (methodCallExpr.Method.DeclaringType.Name == "Enumerable" && methodCallExpr.Method.Name == "Contains")
            {
                return true;
            }

            return false;
        }

        public System.Linq.Expressions.Expression<Action> GetAction(System.Linq.Expressions.Expression expression)
        {
            var methodCallExpr = (MethodCallExpression)expression;

            return AssertBuilderFactory.GetAssertBuilder().GetCollectionContains(methodCallExpr.Arguments[0], methodCallExpr.Arguments[1]);
        }
    }

    public class CollectionContains : IExpressionTypeToAction
    {

        public bool IsValid(Expression expr)
        {
            if (expr.NodeType != ExpressionType.Call)
            {
                return false;
            }

            var methodCallExpr = (MethodCallExpression)expr;

            if (methodCallExpr.Method.DeclaringType.FullName.StartsWith("System.Collections.Generic") && methodCallExpr.Method.Name == "Contains")
            {
                return true;
            }

            return false;
        }

        public Expression<Action> GetAction(Expression expression)
        {
            var methodCallExpr = (MethodCallExpression)expression;

            return AssertBuilderFactory.GetAssertBuilder().GetCollectionContains(methodCallExpr.Object, methodCallExpr.Arguments[0]);
        }
    }
}
