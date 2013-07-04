using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace AssertHelper.Core
{
    internal class AssertBuilder
    {
        private static readonly MethodInfo IsFalse;
        private static readonly MethodInfo IsTrue;
        private static readonly MethodInfo AreEqual;
        private static readonly MethodInfo AreNotEqual;
        private static readonly MethodInfo IsInstanceOfType;
        private static readonly MethodInfo IsNotInstanceOfType;

        static AssertBuilder()
        {
            var assertType = AppDomain.CurrentDomain.GetAssemblies()
                                      .Where(a => a.GetName().Name == "nunit.framework")
                                      .SelectMany(a => a.GetTypes())
                                      .Single(t => t.Name == "Assert");

            IsFalse = assertType.GetMethod("IsFalse", new[] { typeof(bool) });
            IsTrue = assertType.GetMethod("IsTrue", new[] { typeof(bool) });
            AreEqual = assertType.GetMethod("AreEqual", new[] { typeof(object), typeof(object) });
            AreNotEqual = assertType.GetMethod("AreNotEqual", new[] { typeof(object), typeof(object) });
            IsInstanceOfType = assertType.GetMethod("IsInstanceOf", new[] { typeof(Type), typeof(object) });
            IsNotInstanceOfType = assertType.GetMethod("IsNotInstanceOf", new[] { typeof(Type), typeof(object) });
        }

        public Expression<Action> GetAreEqualAction(Expression left, Expression right)
        {
            return Expression.Lambda<Action>(Expression.Call(AreEqual, Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object))));
        }

        public Expression<Action> GetAreNotEqualAction(Expression left, Expression right)
        {
            return Expression.Lambda<Action>(Expression.Call(AreNotEqual, Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object))));
        }

        public Expression<Action> GetIsTrueAction(Expression expression)
        {
            return Expression.Lambda<Action>(Expression.Call(IsTrue, expression));
        }

        public Expression<Action> GetIsFalseAction(Expression expression)
        {
            return Expression.Lambda<Action>(Expression.Call(IsFalse, expression));
        }

       public Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression)
        {
            return Expression.Lambda<Action>(Expression.Call(IsInstanceOfType, Expression.Constant(typeOperand), expression));
        }
    }
}
