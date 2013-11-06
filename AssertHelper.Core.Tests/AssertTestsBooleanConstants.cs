using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsBooleanConstants
    {
        [Test]
        public void This_PassSingleValue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => value);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(value));
        }

        [Test]
        public void This_PassNot_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => !value);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsFalse(value));
        }

        [Test]
        public void This_PassLeftValueEqualToTrue_AssertTrueIsCalled()
        {
            bool value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => value == true);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(value));
        }

        [Test]
        public void This_PassLeftValueEqualToFalse_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => value == false);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsFalse(value));
        }

        [Test]
        public void This_PassRightValueEqualToTrue_AssertTrueIsCalled()
        {
            bool value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => true == value);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(value));
        }

        [Test]
        public void This_PassRightValueEqualToFalse_AssertFalseIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => false == value);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsFalse(value));
        }

        [Test]
        public void This_PassLeftValueNotEqualToTrue_AssertFalseIsCalled()
        {
            bool value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => value != true);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsFalse(value));
        }

        [Test]
        public void This_PassLeftValueNotEqualToFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => value != false);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(value));
        }

        [Test]
        public void This_PassRightValueNotEqualToTrue_AssertFalseIsCalled()
        {
            bool value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => true != value);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsFalse(value));
        }

        [Test]
        public void This_PassRightValueNotEqualToFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => false != value);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(value));
        }
        
        [Test]
        public void This_PassRightFalseOrValue_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => value || false);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(false || value));
        }
        
        [Test]
        public void This_PassRightValueOrFalse_AssertTrueIsCalled()
        {
            var value = DummyCreator.GetBooleanValue();

            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            Assert.This(() => false || value);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.IsTrue(false || value));
        }
    }
}
