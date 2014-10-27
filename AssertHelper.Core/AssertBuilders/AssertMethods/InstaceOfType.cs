using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class InstaceOfType : AssertMethodBase, IBinaryAssertMethod<Type, Expression>
    {
        public InstaceOfType(Type assertType)
            : base(assertType, "IsInstanceOf", new[] { typeof(Type), typeof(object) })
        { }

        public Expression<Action> Assert(Type type, Expression expression)
        {
            return GetExpression(Expression.Constant(type), expression);
        }
    }
}