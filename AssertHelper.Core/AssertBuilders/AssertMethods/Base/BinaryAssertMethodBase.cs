using System;
using System.Linq.Expressions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods.Base
{
    internal abstract class BinaryAssertMethodBase : AssertMethodBase, IBinaryAssertMethod
    {
        protected BinaryAssertMethodBase(Type assertType, string methodName, Type[] methodParametersTypes)
            : base(assertType, methodName, methodParametersTypes)
        {
        }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            // TODO: need to output lamda string
            return GetExpression(left, right);
        }
    }
}