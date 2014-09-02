using System;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsNotNull : AssertTestBase
    {
        protected override Action FailedAssertionAction
        {
            get { return () => Assert.IsNotNull(null); }
        }

        [Test]
        public void That_CompareValueNotEqualToNull_FinishNormally()
        {
            var val = new object();

            Expect.That(() => val != null);
        }

        [Test]
        public void That_CompareValueNotEqualToNull_AssertIsNullCalled()
        {
            object val = null;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => val != null));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }

        [Test]
        public void That_CompareValueNotEqualToNullReversed_AssertIsNullCalled()
        {
            object val = null;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => null != val));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }
    }
}