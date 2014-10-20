﻿using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsAreNotEqual : FakeAssertBuilderTests
    {
        [Test]
        public void That_PassTwoValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetReferenceObject1();
            var expected = DummyCreator.GetReferenceObject1();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreNotEqualAction(A<Expression>._, A<Expression>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value != expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }

        [Test]
        public void That_PassTwoStringValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetString();
            var expected = value;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreNotEqualAction(A<Expression>._, A<Expression>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value != expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }

        [Test]
        public void That_PassTwoIntValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetInt();
            var expected = DummyCreator.GetInt();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreNotEqualAction(A<Expression>._, A<Expression>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value != expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }

        [Test]
        public void That_PassTwoDoubleValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetDouble();
            var expected = value;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var assertAreEqualValidator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetAreNotEqualAction(A<Expression>._, A<Expression>._)).AddAssertValidation(assertAreEqualValidator);

            Expect.That(() => value != expected);

            assertAreEqualValidator.WasAssertCalledWithArguments(expected, value);
        }
    }
}