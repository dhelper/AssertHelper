using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsNull
    {
        [Test]
        public void That_CompareValueEqualToNull_AssertIsNullCalled()
        {
            Isolate.Fake.StaticMethods<Assert>();

            var val = new object();
            Expect.That(() => val == null);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.IsNull(val));
        }

        [Test]
        public void That_CompareValueEqualToNullReversed_AssertIsNullCalled()
        {
            Isolate.Fake.StaticMethods<Assert>();

            var val = new object();
            Expect.That(() => null == val);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.IsNull(val));
        }

        [Test]
        public void That_CompareValueNotEqualToNull_AssertIsNullCalled()
        {
            Isolate.Fake.StaticMethods<Assert>();

            var val = new object();
            Expect.That(() => val != null);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.IsNotNull(val));
        }

        [Test]
        public void That_CompareValueNotEqualToNullReversed_AssertIsNullCalled()
        {
            Isolate.Fake.StaticMethods<Assert>();

            var val = new object();
            Expect.That(() => null != val);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.IsNotNull(val));
        }
    }
}
