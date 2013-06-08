using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsEquality
    {
        private object GetReferenceObject()
        {
            return new object();
        }

        [Test]
        public void This_PassTwoValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = GetReferenceObject();
            var value2 = GetReferenceObject();

            Assert.This(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreEqual(value1, value2));
        }

        [Test]
        public void This_PassTwoValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = GetReferenceObject();
            var value2 = GetReferenceObject();

            Assert.This(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreNotEqual(value1, value2));
        }
    }

}
