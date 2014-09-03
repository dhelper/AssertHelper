using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders;
using FakeItEasy;

namespace AssertHelper.Core.Tests
{
    internal class AssertBuilderFactoryForTests : AssertBuilderFactory
    {
        private static IAssertBuilder _fakeAssertBuilder;

        public static void FakeAssertBuilder()
        {
            _fakeAssertBuilder = A.Fake<IAssertBuilder>();
            Expression<Action> emptyAction = () => NOP();
            A.CallTo(_fakeAssertBuilder).WithReturnType<Expression<Action>>().Returns(emptyAction);
            _assertBuilder = _fakeAssertBuilder;
        }

        public static void Restore()
        {
            Reset();
        }

        private static void NOP() { }
    }
}