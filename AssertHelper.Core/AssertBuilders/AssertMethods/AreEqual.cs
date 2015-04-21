using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class AreEqual : AssertMethodBase, IBinaryAssertMethod
    {
        public AreEqual(Type assertType)
            : base(assertType, "AreEqual", new[] { typeof(object), typeof(object), typeof(string) })
        { }

        public Expression<Action> Assert(Expression left, Expression right, string lambda)
        {
            return GetExpression(Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object)), lambda.ToConstantExpression());
        }
    }
}