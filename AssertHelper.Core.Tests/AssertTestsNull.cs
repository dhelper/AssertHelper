using System;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsNull : AssertTestBase
    {
        protected override Action FailedAssertionAction
        {
            get { return () => Assert.IsNull(new object()); }
        }

        [Test]
        public void That_CompareValueEqualToNullANdValudIsNull_FinishNormally()
        {
           object val = null;

            Expect.That(() => val == null);
        }
        
        [Test]
        public void That_CompareValueEqualToNull_AssertIsNullCalled()
        {
           var val = new object();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => val == null));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }
        
        [Test]
        public void That_CompareValueEqualToNullReversed_AssertIsNullCalled()
        {
            var val = new object();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => null == val));

            Assert.That(result.Message, Is.EqualTo(AssertMessage));
        }
    }
}
