using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsInstance
    {
        [Test]
        public void That_UseIsToCheckType_AssertIsInstaceOfCalled()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var val = DummyCreator.GetDummy();

            Expect.That(() => val is DummyClass);

            var type = typeof (DummyClass);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.IsInstanceOf(type, val));
        } 
        
        [Test, Ignore("TODO")]
        public void That_UseIsNotToCheckType_AssertIsInstaceOfCalled()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var val = DummyCreator.GetDummy();

            Expect.That(() => !(val is DummyClass));

            var type = typeof (DummyClass);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.IsNotInstanceOf(type, val));
        }
    }
}
