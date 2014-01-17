using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Collections;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.NUnit
{
    internal class NUnitAssertBuilder : IAssertBuilder
    {    
        private static AssertMethods _assert;
        private static StringAssertMethods _stringAssert;
        private static CollectionAssertMethods _collectionAssert;

        static NUnitAssertBuilder()
        {
            var nunitTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == "nunit.framework")
                .SelectMany(a => a.GetTypes());           

            _assert = new AssertMethods(nunitTypes);
            _stringAssert = new StringAssertMethods(nunitTypes);
            _collectionAssert = new CollectionAssertMethods(nunitTypes);     
           
        }

        public Expression<Action> GetAreEqualAction(Expression left, Expression right)
        {
            return Expression.Lambda<Action>(Expression.Call(_assert.AreEqual, Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object))));
        }

        public Expression<Action> GetAreNotEqualAction(Expression left, Expression right)
        {
            return Expression.Lambda<Action>(Expression.Call(_assert.AreNotEqual, Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object))));
        }

        public Expression<Action> GetIsTrueAction(Expression expression)
        {
            return _assert.IsTrue.CreateExpression(expression);
        }

        public Expression<Action> GetIsFalseAction(Expression expression)
        {
            return Expression.Lambda<Action>(Expression.Call(_assert.IsFalse, expression));
        }

        public Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression)
        {
            return Expression.Lambda<Action>(Expression.Call(_assert.IsInstanceOfType, Expression.Constant(typeOperand), expression));
        }

        public Expression<Action> GetFail(string message)
        {
            return Expression.Lambda<Action>(Expression.Call(_assert.Fail, Expression.Constant(message)));
        }

        public Expression<Action> GetCollectionContains(Expression collection, Expression value)
        {
            return Expression.Lambda<Action>(Expression.Call(_collectionAssert.Contains, Expression.Convert(collection, typeof(IEnumerable)), Expression.Convert(value, typeof(object))));
        }

        public Expression<Action> GetCollectionEquals(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(_collectionAssert.AreEqual, Expression.Convert(expected, typeof(IEnumerable)), Expression.Convert(actual, typeof(IEnumerable))));
        }

        public Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(_collectionAssert.AreNotEqual, Expression.Convert(expected, typeof(IEnumerable)), Expression.Convert(actual, typeof(IEnumerable))));
        }

        public Expression<Action> GetStringContains(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(_stringAssert.Contains, expected, actual));
        }

        public Expression<Action> GetStringStartsWith(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(_stringAssert.StartsWith, expected, actual));
        }

        public Expression<Action> GetStringEndsWith(Expression expected, Expression actual)
        {
            return Expression.Lambda<Action>(Expression.Call(_stringAssert.EndsWith, expected, actual));
        }
    }
}
