using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders
{
    internal class AssertBuilder : IAssertBuilder
    {
        private readonly IUnaryAssertMethod _isTrue;
        private readonly IUnaryAssertMethod _isFalse;
        private readonly IUnaryAssertMethod _isNull;
        private readonly IUnaryAssertMethod _isNotNull;
        private readonly IBinaryAssertMethod _areEqual;
        private readonly IBinaryAssertMethod _areNotEqual;
        private readonly IBinaryAssertMethod<Type, Expression> _isInstanceOfType;
        private readonly IUnaryAssertMethod<string> _fail;

        private readonly IBinaryAssertMethod _stringContains;
        private readonly IBinaryAssertMethod _stringStartsWith;
        private readonly IBinaryAssertMethod _stringEndsWith;

        private readonly IBinaryAssertMethod _collectionContains;
        private readonly IBinaryAssertMethod _collectionAreEqual;
        private readonly IBinaryAssertMethod _collectionAreNotEqual;

        public AssertBuilder(IAssertBuilderFactory assertFactory)
        {
            _areEqual = assertFactory.CreateAreEqual();
            _areNotEqual = assertFactory.CreateAreNotEqual();
            _isTrue = assertFactory.CreateIsTrue();
            _isFalse = assertFactory.CreateIsFalse();
            _isNull = assertFactory.CreateIsNull();
            _isNotNull = assertFactory.CreateIsNotNull();
            _isNotNull = assertFactory.CreateIsNotNull();
            _isInstanceOfType = assertFactory.CreateIsInstanceOfType();
            _fail = assertFactory.CreateFail();

            _stringContains = assertFactory.CreateStringContains();
            _stringStartsWith = assertFactory.CreateStringStartsWith();
            _stringEndsWith = assertFactory.CreateStringEndsWith();

            _collectionContains = assertFactory.CreateCollectionContains();
            _collectionAreEqual = assertFactory.CreateCollectionAreEqual();
            _collectionAreNotEqual = assertFactory.CreateCollectionAreNotEqual();
        }

        public Expression<Action> GetAreEqualAction(Expression left, Expression right, string lambda)
        {
            return _areEqual.Assert(left, right, lambda);
        }

        public Expression<Action> GetAreNotEqualAction(Expression left, Expression right, string lambda)
        {
            return _areNotEqual.Assert(left, right, lambda);
        }

        public Expression<Action> GetIsTrueAction(Expression expression, string lambda)
        {
            return _isTrue.Assert(expression, lambda);
        }

        public Expression<Action> GetIsFalseAction(Expression expression, string lambda)
        {
            return _isFalse.Assert(expression, lambda);
        }

        public Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression, string lambda)
        {
            return _isInstanceOfType.Assert(typeOperand, expression, lambda);
        }

        public Expression<Action> GetFail(string message)
        {
            return _fail.Assert(message, string.Empty);
        }

        public Expression<Action> GetCollectionContains(Expression expected, Expression actual, string lambda)
        {
            return _collectionContains.Assert(expected, actual, lambda);
        }

        public Expression<Action> GetCollectionEquals(Expression expected, Expression actual, string lambda)
        {
            return _collectionAreEqual.Assert(expected, actual, lambda);
        }

        public Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual, string lambda)
        {
            return _collectionAreNotEqual.Assert(expected, actual, lambda);
        }

        public Expression<Action> GetStringContains(Expression expected, Expression actual, string lambda)
        {
            return _stringContains.Assert(expected, actual, lambda);
        }

        public Expression<Action> GetStringStartsWith(Expression expected, Expression actual, string lambda)
        {
            return _stringStartsWith.Assert(expected, actual, lambda);
        }

        public Expression<Action> GetStringEndsWith(Expression expected, Expression actual, string lambda)
        {
            return _stringEndsWith.Assert(expected, actual, lambda);
        }

        public Expression<Action> GetIsNullAction(Expression expression, string lambda)
        {
            return _isNull.Assert(expression, lambda);
        }

        public Expression<Action> GetIsNotNullAction(Expression expression, string lambda)
        {
            return _isNotNull.Assert(expression, lambda);
        }
    }
}