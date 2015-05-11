using AssertHelper.Core;
using NUnit.Framework;

namespace AssertHelper.NUnit.Tests
{
    [TestFixture]
    public class RunInApplicationDomainTests
    {
        [Test]
        [RunInApplicationDomain]
        public void That_RunInApplicationDomain_RunsTest()
        {
            var value = new object();

            Expect.That(() => value != null);
        }
    }
}