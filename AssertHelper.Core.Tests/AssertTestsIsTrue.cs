using System;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsIsTrue : AssertTestBase
    {
        protected override Action FailedAssertionAction
        {
            get { return () => Assert.IsTrue(false); }
        }

        [Test]
        public void That_PassSingleTrueValue_FinishNormally()
        {
            var value = DummyCreator.GetTrueValue();

            Expect.That(() => value);
        }

        [Test]
        public void That_PassSingleValue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_PassLeftValueEqualToTrue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value == true));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));

        }

        [Test]
        public void That_PassRightValueEqualToTrue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => true == value));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }
      

        [Test]
        public void That_PassLeftValueNotEqualToFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var result = Assert.Throws<AssertionException>(() =>Expect.That(() => value != false));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

       

        [Test]
        public void That_PassRightValueNotEqualToFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var result = Assert.Throws<AssertionException>(() =>Expect.That(() => false != value));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_PassRightFalseOrValue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var result = Assert.Throws<AssertionException>(() =>Expect.That(() => value || false));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_PassRightValueOrFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetFalseValue();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => false || value));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }
    }
}
