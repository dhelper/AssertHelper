using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class InstaceOfTypeInv : AssertMethodBase, IBinaryAssertMethod<Type, Expression>
    {
        public InstaceOfTypeInv(Type assertType)
            : base(assertType, "IsInstanceOfType", new[] { typeof(object), typeof(Type), typeof(string) })
        { }

        public Expression<Action> Assert(Type type, Expression expression, string lambda)
        {
            return GetExpression(expression, Expression.Constant(type), lambda.ToConstantExpression());
        }
    }
}