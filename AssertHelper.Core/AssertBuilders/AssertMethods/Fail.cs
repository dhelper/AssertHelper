using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class Fail : AssertMethodBase, IUnaryAssertMethod<string>
    {
        public Fail(Type assertType)
            : base(assertType, "Fail", new[] { typeof(string) })
        {
        }

        public Expression<Action> Assert(string message)
        {
            return GetExpression(Expression.Constant(message));
        }
    }
}