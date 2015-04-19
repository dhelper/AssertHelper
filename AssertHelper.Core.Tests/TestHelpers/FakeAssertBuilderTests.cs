using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    public class FakeAssertBuilderTests
    {
        [TearDown]
        public void RestoreAssertBuilder()
        {
            AssertBuilderFactoryForTests.Restore();
        }
    }
}