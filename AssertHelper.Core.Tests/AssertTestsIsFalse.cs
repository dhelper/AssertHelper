using System;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsIsFalse : AssertTestBase
    {
        protected override Action FailedAssertionAction
        {
            get { return () => Assert.IsFalse(true); }
        }

        [Test]
        public void That_PassNotToFalseValue_FinishNormally()
        {
            var value = DummyCreator.GetFalseValue();

            Expect.That(() => !value);
        }
        
        [Test]
        public void That_PassNot_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => !value));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_PassLeftValueEqualToFalse_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => value == false));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));

        } 
        
        [Test]
        public void That_PassRightValueEqualToFalse_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => false == value));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));

        }

        [Test]
        public void That_PassLeftValueNotEqualToTrue_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var result = Assert.Throws<AssertionException>(() =>Expect.That(() => value != true));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_PassRightValueNotEqualToTrue_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetTrueValue();

            var result = Assert.Throws<AssertionException>(() =>Expect.That(() => true != value));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }
    }
}