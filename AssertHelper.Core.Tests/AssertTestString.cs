using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestString : FakeAssertBuilderTests
    {
        [Test]
        public void That_StringContainsCalled_StringAssertContainedUsed()
        {
            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetStringContains(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => "1234".Contains("5"));

            validator.WasAssertCalledWithArguments("5", "1234");
        }

        [Test]
        public void That_StringStartsWithCalled_StringAssertStartsWithUsed()
        {
            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetStringStartsWith(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => "1234".StartsWith("2"));

            validator.WasAssertCalledWithArguments("2", "1234");
        }

        [Test]
        public void That_StringEndssWithCalled_StringAssertStartsWithUsed()
        {
            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetStringEndsWith(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => "1234".EndsWith("2"));

            validator.WasAssertCalledWithArguments("2", "1234");
        }
    }
}
