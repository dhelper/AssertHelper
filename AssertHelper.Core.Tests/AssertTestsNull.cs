using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsNull : FakeAssertBuilderTests
    {
        [Test]
        public void That_CompareValueEqualToNullAndValueIsNull_AssertIsNullCalled()
        {
            object val = null;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsNullAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => val == null);

            validator.WasAssertCalledWithArguments(val);
        }

        [Test]
        public void That_NullableBoolAndPassEqualToNull_AssertIsNullCalled()
        {
            var value = (bool?)null;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsNullAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value == null);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_CompareValueEqualToNull_AssertIsNullCalled()
        {
            var val = new object();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsNullAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => val == null);

            validator.WasAssertCalledWithArguments(val);
        }

        [Test]
        public void That_CompareValueEqualToNullReversed_AssertIsNullCalled()
        {
            var val = new object();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsNullAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => null == val);

            validator.WasAssertCalledWithArguments(val);
        }
    }
}
