using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsAreEqual : FakeAssertBuilderTests
    {
        [Test]
        public void That_PassTwoValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetReferenceObject1();
            var expected = DummyCreator.GetReferenceObject2();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreEqualAction(A<Expression>._, A<Expression>._, A<string>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value == expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }

        [Test]
        public void That_PassTwoStringValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetString();
            var expected = DummyCreator.GetString();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreEqualAction(A<Expression>._, A<Expression>._, A<string>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value == expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }

        [Test]
        public void That_PassTwoIntValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetInt();
            var expected = DummyCreator.GetInt() + 1;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreEqualAction(A<Expression>._, A<Expression>._, A<string>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value == expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }

        [Test]
        public void That_PassTwoDoubleValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetDouble();
            var expected = DummyCreator.GetDouble() + 1;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreEqualAction(A<Expression>._, A<Expression>._, A<string>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value == expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }
    }
}
