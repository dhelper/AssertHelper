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
        public static Expression<Action> ToExpression(this MethodInfo methodInfo, params Expression[] expressions)
        {
            return Expression.Lambda<Action>(Expression.Call(methodInfo, expressions));
        }        
    }
}
