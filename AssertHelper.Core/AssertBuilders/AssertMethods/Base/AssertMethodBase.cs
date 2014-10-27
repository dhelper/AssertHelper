using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AssertHelper.Core.AssertBuilders.AssertMethods.Base
{
    internal abstract class AssertMethodBase
    {
        private readonly MethodInfo _methodInfo;

        protected AssertMethodBase(Type assertType, string methodName, Type[] methodParametersTypes)
        {
            _methodInfo = assertType.GetMethod(methodName, methodParametersTypes);
        }

        protected Expression<Action> GetExpression(params Expression[] expressions)
        {
            return Expression.Lambda<Action>(Expression.Call(_methodInfo, expressions));
        }
    }
}