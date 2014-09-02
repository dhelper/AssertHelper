using System;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsAreNotEqual : AssertTestBase
    {
        protected override Action FailedAssertionAction
        {
            get
            {
                return () => Assert.AreNotEqual(DummyCreator.GetReferenceObject1(), DummyCreator.GetReferenceObject1());
            }
        }

        [Test]
        public void That_PassTwoValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetReferenceObject1();
            var expected = DummyCreator.GetReferenceObject1();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value != expected));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_PassTwoStringValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetString();
            var expected = value;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value != expected));

            var expectedMessage = GetAssertionMessage(() => Assert.AreNotEqual(expected, value));

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_PassTwoIntValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetInt();
            var expected = DummyCreator.GetInt();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value != expected));

            var expectedMessage = GetAssertionMessage(() => Assert.AreNotEqual(expected, value));

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_PassTwoDoubleValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            var value = DummyCreator.GetDouble();
            var expected = value;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value != expected));

            var expectedMessage = GetAssertionMessage(() => Assert.AreNotEqual(expected, value));

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }
    }
}