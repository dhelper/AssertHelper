using System;
using System.Linq.Expressions;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods.Base
{
    internal abstract class UnaryAssertMethodBase : AssertMethodBase, IUnaryAssertMethod
    {
        protected UnaryAssertMethodBase(Type assertType, string methodName, Type[] methodParametersTypes)
            : base(assertType, methodName, methodParametersTypes)
        {
        }

        public Expression<Action> Assert(Expression expression, string lambda)
        {
            return GetExpression(expression, lambda.ToConstantExpression());
        }
    }
}