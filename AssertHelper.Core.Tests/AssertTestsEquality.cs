using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsEquality
    {
        [Test]
        public void This_PassTwoValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetReferenceObject();
            var value2 = DummyCreator.GetReferenceObject();

            Assert.This(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreEqual(value2, value1));
        }

        [Test]
        public void This_PassTwoValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetReferenceObject();
            var value2 = DummyCreator.GetReferenceObject();

            Assert.This(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreNotEqual(value2, value1));
        }
        
        [Test]
        public void This_PassTwoStringValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetString();
            var value2 = DummyCreator.GetString();

            Assert.This(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreEqual(value2, value1));
        }

        [Test]
        public void This_PassTwoStringValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetString();
            var value2 = DummyCreator.GetString();

            Assert.This(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreNotEqual((object)value2, (object)value1));
        }
        
        [Test]
        public void This_PassTwoIntValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetInt();
            var value2 = DummyCreator.GetInt();

            Assert.This(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreEqual((object)value2, (object)value1));
        }

        [Test]
        public void This_PassTwoIntValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetInt();
            var value2 = DummyCreator.GetInt();

            Assert.This(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreNotEqual((object)value2, (object)value1));
        }
        
        [Test]
        public void This_PassTwoDoubleValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetDouble();
            var value2 = DummyCreator.GetDouble();

            Assert.This(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreEqual(value2, value1));
        }

        [Test]
        public void This_PassTwoDoubleValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetDouble();
            var value2 = DummyCreator.GetDouble();

            Assert.This(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => NUnit.Framework.Assert.AreNotEqual((object)value2, (object)value1));
        }
    }
}
