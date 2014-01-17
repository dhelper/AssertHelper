using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssertHelper.Core.Extensions
{
    internal static class MethodInfoExtensions
    {
        public static Expression<Action> CreateExpression(this MethodInfo methodInfo, Expression expression)
        {
            return Expression.Lambda<Action>(Expression.Call(methodInfo, expression));
        }
    }
}
