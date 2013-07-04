using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsInstance
    {
        [Test]
        public void This_UseIsToCheckType_AssertIsInstaceOfCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var val = DummyCreator.GetDummy();

            Assert.This(() => val is DummyClass);

            var type = typeof (DummyClass);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsInstanceOf(type, val));
        } 
        
        [Test, Ignore("TODO")]
        public void This_UseIsNotToCheckType_AssertIsInstaceOfCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var val = DummyCreator.GetDummy();

            Assert.This(() => !(val is DummyClass));

            var type = typeof (DummyClass);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsNotInstanceOf(type, val));
        }
    }
}
