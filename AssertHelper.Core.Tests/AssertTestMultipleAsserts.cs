﻿using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestMultipleAsserts
    {
        [Test]
        public void That_HaveOneEqualityAndOneBooleanInsideAssertBlock_BothConditionsAreTested()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var obj1 = DummyCreator.GetReferenceObject();
            var obj2 = DummyCreator.GetReferenceObject();
            var b1 = DummyCreator.GetBooleanValue();

            Expect.That(() => obj1 == obj2 && b1);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(b1));
            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreEqual(obj2, obj1));
        }

        [Test]
        public void That_HaveTwoBooleanInsideAssertBlock_BothConditionsAreTested()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var b1 = DummyCreator.GetBooleanValue();
            var b2 = DummyCreator.GetBooleanValue();

            Expect.That(() => b1 && b2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(b1));
            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(b2));
        }

        [Test]
        public void That_HaveTwoBooleanInsideAssertBlockOneFail_BothConditionsAreTested()
        {
            Isolate.WhenCalled(() => NUnit.Framework.Assert.IsTrue(false)).WillThrow(new AssertionException("This is a test"));

            var b1 = DummyCreator.GetTrueValue();
            var b2 = DummyCreator.GetFalseValue();

            NUnit.Framework.Assert.Throws<AssertionException>(() => Expect.That(() => b1 && b2));

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(b1));
            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(b2));
        }

        [Test]
        public void That_HaveThreeBooleanInsideAssertBlock_BothConditionsAreTested()
        {
            Isolate.WhenCalled(() => NUnit.Framework.Assert.IsTrue(false)).IgnoreCall();

            var b1 = DummyCreator.GetBooleanValue();
            var b2 = DummyCreator.GetBooleanValue();
            var b3 = DummyCreator.GetBooleanValue();

            Expect.That(() => b1 && b2 && b3);

            int timesCalled = Isolate.Verify.GetTimesCalled(() => NUnit.Framework.Assert.IsTrue(false));

            NUnit.Framework.Assert.AreEqual(3, timesCalled);
        }
    }
}
