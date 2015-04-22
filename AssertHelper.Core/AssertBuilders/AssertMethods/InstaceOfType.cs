using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class InstaceOfType : AssertMethodBase, IBinaryAssertMethod<Type, Expression>
    {
        public InstaceOfType(Type assertType)
            : base(assertType, "IsInstanceOf", new[] { typeof(Type), typeof(object), typeof(string) })
        { }

        public Expression<Action> Assert(Type type, Expression expression, string lambda)
        {
            return GetExpression(Expression.Constant(type), expression, lambda.ToConstantExpression());
        }
    }
}