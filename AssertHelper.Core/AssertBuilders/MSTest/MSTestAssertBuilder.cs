using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssertHelper.Core.AssertBuilders.NUnit;
using AssertHelper.Core.Extensions;

namespace AssertHelper.Core.AssertBuilders.MSTest
{
    class MSTestAssertBuilder : IAssertBuilder
    {
        private readonly MethodInfo _isFalse;
        private readonly MethodInfo _isTrue;
        private readonly MethodInfo _isNull;
        private readonly MethodInfo _isNotNull;
        private readonly MethodInfo _areEqual;
        private readonly MethodInfo _areNotEqual;
        private readonly MethodInfo _isInstanceOfType;
        private readonly MethodInfo _fail;

        private readonly MethodInfo _stringContains;
        private readonly MethodInfo _stringStartsWith;
        private readonly MethodInfo _stringEndsWith;

        private readonly MethodInfo _collectionContains;
        private readonly MethodInfo _collectionAreEqual;
        private readonly MethodInfo _collectionAreNotEqual;

        public MSTestAssertBuilder()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == "Microsoft.VisualStudio.QualityTools.UnitTestFramework")
                .SelectMany(a => a.GetTypes());

            var assertType = types.Single(t => t.Name == "Assert");

            _isFalse = assertType.GetMethod("IsFalse", new[] { typeof(bool) });
            _isTrue = assertType.GetMethod("IsTrue", new[] { typeof(bool) });
            _isNull = assertType.GetMethod("IsNull", new[] { typeof(object) });
            _isNotNull = assertType.GetMethod("IsNotNull", new[] { typeof(object) });
            _areEqual = assertType.GetMethod("AreEqual", new[] { typeof(object), typeof(object) });
            _areNotEqual = assertType.GetMethod("AreNotEqual", new[] { typeof(object), typeof(object) });
            _isInstanceOfType = assertType.GetMethod("IsInstanceOfType", new[] { typeof(object), typeof(Type) });
            _fail = assertType.GetMethod("Fail", new[] { typeof(string) });

            var stringAssertType = types.Single(type => type.Name == "StringAssert");
            _stringContains = stringAssertType.GetMethod("Contains", new[] { typeof(string), typeof(string) });
            _stringStartsWith = stringAssertType.GetMethod("StartsWith", new[] { typeof(string), typeof(string) });
            _stringEndsWith = stringAssertType.GetMethod("EndsWith", new[] { typeof(string), typeof(string) });

            var collectionAssertType = types.Single(type => type.Name == "CollectionAssert");
            _collectionContains = collectionAssertType.GetMethod("Contains", new[] { typeof(ICollection), typeof(object) });
            _collectionAreEqual = collectionAssertType.GetMethod("AreEqual", new[] { typeof(ICollection), typeof(ICollection) });
            _collectionAreNotEqual = collectionAssertType.GetMethod("AreNotEqual", new[] { typeof(ICollection), typeof(ICollection) });
        }


        public Expression<Action> GetAreEqualAction(Expression left, Expression right)
        {
            return _areEqual.ToExpression(Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object)));
        }

        public Expression<Action> GetAreNotEqualAction(Expression left, Expression right)
        {
            return _areNotEqual.ToExpression(Expression.Convert(left, typeof(object)), Expression.Convert(right, typeof(object)));
        }

        public Expression<Action> GetIsTrueAction(Expression expression)
        {
            return _isTrue.ToExpression(expression);
        }

        public Expression<Action> GetIsFalseAction(Expression expression)
        {
            return _isFalse.ToExpression(expression);
        }

        public Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression)
        {
            return _isInstanceOfType.ToExpression(expression, Expression.Constant(typeOperand));
        }

        public Expression<Action> GetFail(string message)
        {
            return _fail.ToExpression(Expression.Constant(message));
        }

        public Expression<Action> GetCollectionContains(Expression expected, Expression actual)
        {
            return _collectionContains.ToExpression(Expression.Convert(expected, typeof(ICollection)), Expression.Convert(actual, typeof(object)));
        }

        public Expression<Action> GetCollectionEquals(Expression expected, Expression actual)
        {
            return _collectionAreEqual.ToExpression(Expression.Convert(expected, typeof(ICollection)), Expression.Convert(actual, typeof(ICollection)));
        }

        public Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual)
        {
            return _collectionAreNotEqual.ToExpression(Expression.Convert(expected, typeof(ICollection)), Expression.Convert(actual, typeof(ICollection)));
        }

        public Expression<Action> GetStringContains(Expression expected, Expression actual)
        {
            return _stringContains.ToExpression(expected, actual);
        }

        public Expression<Action> GetStringStartsWith(Expression expected, Expression actual)
        {
            return _stringStartsWith.ToExpression(expected, actual);
        }

        public Expression<Action> GetStringEndsWith(Expression expected, Expression actual)
        {
            return _stringEndsWith.ToExpression(expected, actual);
        }

        public Expression<Action> GetIsNullAction(Expression expression)
        {
            return _isNull.ToExpression(expression);
        }

        public Expression<Action> GetIsNotNullAction(Expression expression)
        {
            return _isNotNull.ToExpression(expression);
        }
    }
}
