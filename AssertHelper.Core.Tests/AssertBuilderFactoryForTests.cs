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
}