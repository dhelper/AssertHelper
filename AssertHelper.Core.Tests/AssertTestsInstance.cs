using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsInstance
    {
        [Test]
        public void That_UseIsToCheckType_AssertIsInstaceOfCalled()
        {
            var val = DummyCreator.GetDummy();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => val is string));

            var expected = AssertTestBase.GetAssertionMessage(() => Assert.IsInstanceOf(typeof(string), val));

            Assert.That(result.Message, Is.EqualTo(expected));
        } 
    }
}
