using System;
using AssertHelper.Core;
using NUnit.Framework;

namespace AssertHelper.NUnit.Tests
{
    public abstract class SpecificAssertTestsBase
    {
        protected abstract Action<int, int> AssertEqualAction { get; }
        protected abstract Action<int, int> AssertNotEqualAction { get; }
        protected abstract Action<bool> AssertIsTrueAction { get; }
        protected abstract Action<bool> AssertIsFalseAction { get; }

        [Test]
        public void That_CompareTwoNumbers_ReturnSameErrorMessageAsAreEqual()
        {
            int actual = 1;
            int expected = 2;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == expected));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertEqualAction(expected, actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_CheckIfTwoValuesAreDifferent_ReturnSameErrorMessageAsAreNotEqual()
        {
            int actual = 1;
            int expected = 1;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual != expected));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertNotEqualAction(expected, actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_CompareValueToTrue_ReturnSameErrorMessageAsIsTrue()
        {
            bool actual = false;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == true));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertIsTrueAction(actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_CompareValueToFalse_ReturnSameErrorMessageAsIsFalse()
        {
            bool actual = true;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == false));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertIsFalseAction(actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }
    }


    [TestFixture]
    public class NUnitTests : SpecificAssertTestsBase
    {

       

        protected override Action<int, int> AssertEqualAction
        {
            get { return Assert.AreEqual; }
        }

        protected override Action<int, int> AssertNotEqualAction
        {
            get { return Assert.AreNotEqual; }
        }

        protected override Action<bool> AssertIsTrueAction
        {
            get { return Assert.IsTrue; }
        }

        protected override Action<bool> AssertIsFalseAction
        {
            get { return Assert.IsFalse; }
        }
    }
}
