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

            Isolate.Verify.WasCalledWithExactArguments(() => StringAssert.Contains("1234", "2"));
        }
    }
}
