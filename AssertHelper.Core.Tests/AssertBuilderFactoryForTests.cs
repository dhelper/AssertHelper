using System;
using System.Linq.Expressions;
using System.Reflection;
using AssertHelper.Core.AssertBuilders;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace AssertHelper.Core.Tests
{
    internal class AssertBuilderFactoryForTests : AssertBuilderFactory
    {
        public static IAssertBuilder FakeAssertBuilder()
        {
            var fakeAssertBuilder = A.Fake<IAssertBuilder>();
            Expression<Action> emptyAction = () => NOP();
            A.CallTo(fakeAssertBuilder).WithReturnType<Expression<Action>>().Returns(emptyAction);
            _assertBuilder = fakeAssertBuilder;

            return fakeAssertBuilder;
        }

       
        public static void Restore()
        {
            Reset();
        }

        private static void NOP() { }

        
    }

    public static class FakeAssertBuilderExtensions
    {
        public static void AddAssertValidation(this IReturnValueArgumentValidationConfiguration<Expression<Action>> callSpecification, CallValidator callValidator)
        {
            callSpecification.Invokes(call => callValidator.SetArguments(call.Arguments)).Returns(() => MarkIsTrue(callValidator));
        }

        public static void MarkIsTrue(CallValidator callValidator)
        {
            callValidator.WasCalled = true;
        }
    }


    internal class FakeAssertBuilder : IAssertBuilder
    {
        public bool IsValid()
        {
            return true;
        }

        public Expression<Action> GetAreEqualAction(Expression left, Expression right)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetAreNotEqualAction(Expression left, Expression right)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetIsTrueAction(Expression expression)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetIsFalseAction(Expression expression)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetIsInstanceOf(Type typeOperand, Expression expression)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetFail(string message)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetCollectionContains(Expression expected, Expression actual)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetCollectionEquals(Expression expected, Expression actual)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetCollectionNotEquals(Expression expected, Expression actual)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetStringContains(Expression expected, Expression actual)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetStringStartsWith(Expression expected, Expression actual)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetStringEndsWith(Expression expected, Expression actual)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetIsNullAction(Expression expression)
        {
            throw new NotImplementedException();
        }

        public Expression<Action> GetIsNotNullAction(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}