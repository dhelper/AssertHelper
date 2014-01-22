using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsEquality
    {
        [Test]
        public void That_PassTwoValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var value1 = DummyCreator.GetReferenceObject();
            var value2 = DummyCreator.GetReferenceObject();

            Expect.That(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreEqual(value2, value1));
        }

        [Test]
        public void That_PassTwoValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var value1 = DummyCreator.GetReferenceObject();
            var value2 = DummyCreator.GetReferenceObject();

            Expect.That(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreNotEqual(value2, value1));
        }
        
        [Test]
        public void That_PassTwoStringValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var value1 = DummyCreator.GetString();
            var value2 = DummyCreator.GetString();

            Expect.That(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreEqual(value2, value1));
        }

        [Test]
        public void That_PassTwoStringValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetString();
            var value2 = DummyCreator.GetString();

            Expect.That(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreNotEqual((object)value2, (object)value1));
        }
        
        [Test]
        public void That_PassTwoIntValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var value1 = DummyCreator.GetInt();
            var value2 = DummyCreator.GetInt();

            Expect.That(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreEqual((object)value2, (object)value1));
        }

        [Test]
        public void That_PassTwoIntValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var value1 = DummyCreator.GetInt();
            var value2 = DummyCreator.GetInt();

            Expect.That(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreNotEqual((object)value2, (object)value1));
        }
        
        [Test]
        public void That_PassTwoDoubleValuesWithEqualSign_TransformToAssertEqual()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.Assert));

            var value1 = DummyCreator.GetDouble();
            var value2 = DummyCreator.GetDouble();

            Expect.That(() => value1 == value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreEqual(value2, value1));
        }

        [Test]
        public void That_PassTwoDoubleValuesWithNotEqualSign_TransformToAssertNotEqual()
        {
            Isolate.Fake.StaticMethods(typeof(Assert));

            var value1 = DummyCreator.GetDouble();
            var value2 = DummyCreator.GetDouble();

            Expect.That(() => value1 != value2);

            Isolate.Verify.WasCalledWithExactArguments(() => Assert.AreNotEqual((object)value2, (object)value1));
        }
    }
}
