using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsIsFalse : FakeAssertBuilderTests
    {
        [Test]
        public void That_PassNotToFalseValue_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsFalseAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => !value);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_NullableBoolAndPassEqualToFalseValue_AreEqualCalled()
        {
            var value = (bool?)DummyCreator.GetTrueValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreEqualAction(A<Expression>._,A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value == false);

            validator.WasAssertCalledWithArguments<bool?, bool?>(false, value);
        }

        [Test]
        public void That_PassNot_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsFalseAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => !value);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_PassLeftValueEqualToFalse_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsFalseAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value == false);

            validator.WasAssertCalledWithArguments(value);

        }

        [Test]
        public void That_PassRightValueEqualToFalse_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsFalseAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => false == value);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_PassLeftValueNotEqualToTrue_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsFalseAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => value != true);

            validator.WasAssertCalledWithArguments(value);
        }

        [Test]
        public void That_PassRightValueNotEqualToTrue_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsFalseAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => true != value);

            validator.WasAssertCalledWithArguments(value);
        }
    }
}