using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Collections;

namespace AssertHelper.Core.AssertBuilders
{
    internal class NUnitAssertBuilder : IAssertBuilder
    {
        private static readonly MethodInfo IsFalse;
        private static readonly MethodInfo IsTrue;
        private static readonly MethodInfo AreEqual;
        private static readonly MethodInfo AreNotEqual;
        private static readonly MethodInfo IsInstanceOfType;
        private static readonly MethodInfo IsNotInstanceOfType;
        private static readonly MethodInfo Fail;
        private static readonly MethodInfo CollectionContains;

        static NUnitAssertBuilder()
        {
            var nunitAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == "nunit.framework");
            var assertType = nunitAssembly.SelectMany(a => a.GetTypes()).Single(t => t.Name == "Assert");
             var collectionAssertType = nunitAssembly.SelectMany(a => a.GetTypes()).Single(t => t.Name == "CollectionAssert");

            IsFalse = assertType.GetMethod("IsFalse", new[] { typeof(bool) });
            IsTrue = assertType.GetMethod("IsTrue", new[] { typeof(bool) });
            AreEqual = assertType.GetMethod("AreEqual", new[] { typeof(object), typeof(object) });
            AreNotEqual = assertType.GetMethod("AreNotEqual", new[] { typeof(object), typeof(object) });
            IsInstanceOfType = assertType.GetMethod("IsInstanceOf", new[] { typeof(Type), typeof(object) });
            IsNotInstanceOfType = assertType.GetMethod("IsNotInstanceOf", new[] { typeof(Type), typeof(object) });
            Fail = assertType.GetMethod("Fail", new[] { typeof(string) });

           CollectionContains = collectionAssertType.GetMethod("Contains", new[]{typeof(IEnumerable), typeof(object)});
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

        public Expression<Action> GetFail(string message)
        {
            return Expression.Lambda<Action>(Expression.Call(Fail, Expression.Constant(message)));
        }

        public Expression<Action> GetCollectionContains(Expression collection, Expression value)
        {
            return Expression.Lambda<Action>(Expression.Call(CollectionContains, Expression.Convert(collection, typeof(IEnumerable)), Expression.Convert(value, typeof(object))));
        }
    }
}
