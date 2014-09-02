using System;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsAreEqual : AssertTestBase
    {
        protected override Action FailedAssertionAction
        {
            get
            {
                return () => Assert.AreEqual(DummyCreator.GetReferenceObject2(), DummyCreator.GetReferenceObject1());
            }
        }

        [Test]
        public void That_PassTwoValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetReferenceObject1();
            var expected = DummyCreator.GetReferenceObject2();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value == expected));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_PassTwoStringValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetString();
            var expected = DummyCreator.GetString();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value == expected));

            var expectedMessage = GetAssertionMessage(() => Assert.AreEqual(expected, value));

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }



        [Test]
        public void That_PassTwoIntValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetInt();
            var expected = DummyCreator.GetInt() + 1;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value == expected));

            var expectedMessage = GetAssertionMessage(() => Assert.AreEqual(expected, value));

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_PassTwoDoubleValuesWithEqualSign_TransformToAssertEqual()
        {
            var value = DummyCreator.GetDouble();
            var expected = DummyCreator.GetDouble() + 1;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value == expected));

            var expectedMessage = GetAssertionMessage(() => Assert.AreEqual(expected, value));

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }
    }
}
