using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class AreEqual : AssertMethodBase, IBinaryAssertMethod
    {
        public AreEqual(Type assertType)
            : base(assertType, "AreEqual", new[] { typeof(object), typeof(object) })
        { }

        public Expression<Action> Assert(Expression left, Expression right)
        {
            return GetExpression(Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object)));
        }
    }
}