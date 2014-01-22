using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestString
    {
        [Test]
        public void That_StringContainsCalled_StringAssertContainedUsed()
        {
            Isolate.Fake.StaticMethods(typeof(StringAssert));

            Expect.That(() => "1234".Contains("2"));

            Isolate.Verify.WasCalledWithExactArguments(() => StringAssert.Contains("2", "1234"));
        }

        [Test]
        public void That_StringStartsWithCalled_StringAssertStartsWithUsed()
        {
            Isolate.Fake.StaticMethods(typeof(StringAssert));

            Expect.That(() => "1234".StartsWith("2"));

            Isolate.Verify.WasCalledWithExactArguments(() => StringAssert.StartsWith("2", "1234"));
        }

        [Test]
        public void That_StringEndssWithCalled_StringAssertStartsWithUsed()
        {
            Isolate.Fake.StaticMethods(typeof(StringAssert));

            Expect.That(() => "1234".EndsWith("2"));

            Isolate.Verify.WasCalledWithExactArguments(() => StringAssert.EndsWith("2", "1234"));
        }
    }
}
