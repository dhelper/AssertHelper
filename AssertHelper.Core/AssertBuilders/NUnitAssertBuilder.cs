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
        private static readonly MethodInfo StringContains;
        private static readonly MethodInfo StringStartsWith;
        private static readonly MethodInfo StringEndsWith;         

        static NUnitAssertBuilder()
        {
            var nunitTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == "nunit.framework")
                .SelectMany(a => a.GetTypes());

            var assertType = nunitTypes.Single(t => t.Name == "Assert");

            IsFalse = assertType.GetMethod("IsFalse", new[] { typeof(bool) });
            IsTrue = assertType.GetMethod("IsTrue", new[] { typeof(bool) });
            AreEqual = assertType.GetMethod("AreEqual", new[] { typeof(object), typeof(object) });
            AreNotEqual = assertType.GetMethod("AreNotEqual", new[] { typeof(object), typeof(object) });
            IsInstanceOfType = assertType.GetMethod("IsInstanceOf", new[] { typeof(Type), typeof(object) });
            IsNotInstanceOfType = assertType.GetMethod("IsNotInstanceOf", new[] { typeof(Type), typeof(object) });
            Fail = assertType.GetMethod("Fail", new[] { typeof(string) });

            var collectionAssertType = nunitTypes.Single(type => type.Name == "CollectionAssert");
           CollectionContains = collectionAssertType.GetMethod("Contains", new[]{typeof(IEnumerable), typeof(object)});

            var stringAssertType = nunitTypes.Single(type => type.Name == "StringAssert");
            StringContains = stringAssertType.GetMethod("Contains", new[] {typeof (string), typeof (string)});
            StringStartsWith = stringAssertType.GetMethod("StartsWith", new[] { typeof(string), typeof(string) });
            StringEndsWith = stringAssertType.GetMethod("EndsWith", new[] { typeof(string), typeof(string) });
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
        public Expression<Action> GetStringContains(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(StringContains, expected, actual));
        }

        public Expression<Action> GetStringStartsWith(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(StringStartsWith, expected, actual));
        }

        public Expression<Action> GetStringEndsWith(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(StringEndsWith, expected, actual));
        }
    }
}
