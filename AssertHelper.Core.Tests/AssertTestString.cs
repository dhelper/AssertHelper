using System;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestString
    {
        [Test]
        public void this_StringContainsCalled_StringAssertContainedUsed()
        {
            Isolate.Fake.StaticMethods(typeof(StringAssert));

            Assert.This(() => "1234".Contains("2"));

            Isolate.Verify.WasCalledWithExactArguments(() => StringAssert.Contains("2", "1234"));
        }

        [Test]
        public void this_StringStartsWithCalled_StringAssertStartsWithUsed()
        {
            Isolate.Fake.StaticMethods(typeof(StringAssert));

            Assert.This(() => "1234".StartsWith("2"));

            Isolate.Verify.WasCalledWithExactArguments(() => StringAssert.StartsWith("2", "1234"));
        }

        [Test]
        public void this_StringEndssWithCalled_StringAssertStartsWithUsed()
        {
            Isolate.Fake.StaticMethods(typeof(StringAssert));

            Assert.This(() => "1234".EndsWith("2"));

            Isolate.Verify.WasCalledWithExactArguments(() => StringAssert.EndsWith("2", "1234"));
        }
    }
}
