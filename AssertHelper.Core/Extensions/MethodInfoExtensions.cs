using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AssertHelper.Core.Extensions
{
    internal static class MethodInfoExtensions
    {
        public static Expression<Action> ToExpression(this MethodInfo methodInfo, params Expression[] expressions)
        {
            return Expression.Lambda<Action>(Expression.Call(methodInfo, expressions));
        }        
    }
}
