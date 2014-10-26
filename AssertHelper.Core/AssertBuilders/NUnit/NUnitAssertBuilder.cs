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
        private readonly AssertMethods _assert;
        private readonly StringAssertMethods _stringAssert;
        private readonly CollectionAssertMethods _collectionAssert;

        public NUnitAssertBuilder()
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
            return _assert.AreEqual.ToExpression(Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object)));
        }

        public Expression<Action> GetAreNotEqualAction(Expression left, Expression right)
        {
            return _assert.AreNotEqual.ToExpression(Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object)));
        }

        public Expression<Action> GetIsTrueAction(Expression expression)
        {
            return _assert.IsTrue.ToExpression(expression);
        }

        public Expression<Action> GetIsFalseAction(Expression expression)
        {
            return _assert.IsFalse.ToExpression(expression);
        }

        public Expression<Action> GetIsNullAction(Expression expression)
        {
            return _assert.IsNull.ToExpression(expression);
        }

        public Expression<Action> GetIsNotNullAction(Expression expression)
        {
            return _assert.IsNotNull.ToExpression(expression);
        }

        public Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression)
        {
            return _assert.IsInstanceOfType.ToExpression(Expression.Constant(typeOperand), expression);
        }

        public Expression<Action> GetFail(string message)
        {
            return _assert.Fail.ToExpression(Expression.Constant(message));
        }

        public Expression<Action> GetCollectionContains(Expression expected, Expression actual)
        {
            return _collectionAssert.Contains.ToExpression(Expression.Convert(expected, typeof(IEnumerable)), Expression.Convert(actual, typeof(object)));
        }

        public Expression<Action> GetCollectionEquals(Expression expected, Expression actual)
        {
            return _collectionAssert.AreEqual.ToExpression(Expression.Convert(expected, typeof(IEnumerable)), Expression.Convert(actual, typeof(IEnumerable)));
        }

        public Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual)
        {
            return _collectionAssert.AreNotEqual.ToExpression(Expression.Convert(expected, typeof(IEnumerable)), Expression.Convert(actual, typeof(IEnumerable)));
        }

        public Expression<Action> GetStringContains(Expression expected, Expression actual)
        {
            return _stringAssert.Contains.ToExpression(expected, actual);
        }

        public Expression<Action> GetStringStartsWith(Expression expected, Expression actual)
        {
            return _stringAssert.StartsWith.ToExpression(expected, actual);
        }

        public Expression<Action> GetStringEndsWith(Expression expected, Expression actual)
        {
            return _stringAssert.EndsWith.ToExpression(expected, actual);
        }
    }
}
