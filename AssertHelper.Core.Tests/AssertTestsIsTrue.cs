using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsIsTrue : FakeAssertBuilderTests
    {
        [Test]
        public void That_PassSingleTrueValue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            Expect.That(() => value);
        }

        [Test]
        public void That_NullableBoolAndPassEqualToTrue_AssertEqualCalled()
        {
            var value = (bool?)DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreEqualAction(A<Expression>._, A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value == true);

            validator.WasAssertCalledWithArguments<bool?, bool?>(true, value);
        }

        [Test]
        public void That_PassSingleValue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_PassLeftValueEqualToTrue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value == true);

            validator.WasAssertCalledWithArguments(value);

        }

        [Test]
        public void That_PassRightValueEqualToTrue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => true == value);

            validator.WasAssertCalledWithArguments(value);
        }


        [Test]
        public void That_PassLeftValueNotEqualToFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value != false);

            validator.WasAssertCalledWithArguments(value);
        }
        
        [Test]
        public void That_PassRightValueNotEqualToFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => false != value);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_PassRightFalseOrValue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value || false);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_PassRightValueOrFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => false || value);

            validator.WasAssertCalledWithArguments(value);
        }
    }
}
