using AssertHelper.Core;
using NUnit.Framework;

namespace AssertHelper.NUnit.Tests
{
    [TestFixture]
    public class NUnitTests
    {
        [Test]
        public void That_CompareTwoNumbers_ReturnSameErrorMessageAsAreEqual()
        {
            int actual = 1;
            int expected = 2;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == expected));

            var expectedMessage = Assert.Throws<AssertionException>(() => Assert.AreEqual(expected, actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_CheckIfTwoValuesAreDifferent_ReturnSameErrorMessageAsAreNotEqual()
        {
            int actual = 1;
            int expected = 1;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual != expected));

            var expectedMessage = Assert.Throws<AssertionException>(() => Assert.AreNotEqual(expected, actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_CompareValueToTrue_ReturnSameErrorMessageAsIsTrue()
        {
            bool actual = false;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == true));

            var expectedMessage = Assert.Throws<AssertionException>(() => Assert.IsTrue(actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void That_CompareValueToFalse_ReturnSameErrorMessageAsIsFalse()
        {
            bool actual = true;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == false));

            var expectedMessage = Assert.Throws<AssertionException>(() => Assert.IsFalse(actual)).Message;

            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }
    }
}
