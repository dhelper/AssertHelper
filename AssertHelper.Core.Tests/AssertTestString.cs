using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestString
    {
        [Test]
        public void That_StringContainsCalled_StringAssertContainedUsed()
        {
            var result = Assert.Throws<AssertionException>(() => Expect.That(() => "1234".Contains("5")));
            var expected = AssertTestBase.GetAssertionMessage(() => StringAssert.Contains("5", "1234"));

            Assert.That(result.Message, Is.EqualTo(expected));
        }

        [Test]
        public void That_StringStartsWithCalled_StringAssertStartsWithUsed()
        {
            var result = Assert.Throws<AssertionException>(() => Expect.That(() => "1234".StartsWith("2")));
            var expected = AssertTestBase.GetAssertionMessage(() => StringAssert.StartsWith("2", "1234"));

            Assert.That(result.Message, Is.EqualTo(expected));
        }

        [Test]
        public void That_StringEndssWithCalled_StringAssertStartsWithUsed()
        {

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => "1234".EndsWith("2")));
            var expected = AssertTestBase.GetAssertionMessage(() => StringAssert.EndsWith("2", "1234"));

            Assert.That(result.Message, Is.EqualTo(expected));
        }
    }
}
