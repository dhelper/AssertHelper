using System.Linq.Expressions;
using System.Resources;
using FakeItEasy;
using NUnit.Framework;
using AssertHelper.Core.AssertBuilders;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestMultipleAsserts
    {
        [TearDown]
        public void RestoreAssertBuilder()
        {
            AssertBuilderFactoryForTests.Restore();
        }

        [Test]
        public void That_HaveOneEqualityAndOneBooleanInsideAssertBlock_BothConditionsAreTested()
        {
            AssertBuilderFactoryForTests.FakeAssertBuilder();

            var obj1 = DummyCreator.GetReferenceObject1();
            var obj2 = DummyCreator.GetReferenceObject2();
            var b1 = DummyCreator.GetTrueBooleanValue();

            Expect.That(() => obj1 == obj2 && b1);

            var fakeBuilder = AssertBuilderFactory.GetAssertBuilder();
            A.CallTo(() => fakeBuilder.GetIsTrueAction(A<Expression>._)).MustHaveHappened();
            A.CallTo(() => fakeBuilder.GetAreEqualAction(A<Expression>._, A<Expression>._)).MustHaveHappened();
        }

        [Test]
        public void That_HaveTwoBooleanInsideAssertBlock_BothConditionsAreTested()
        {
            AssertBuilderFactoryForTests.FakeAssertBuilder();            

            var b1 = DummyCreator.GetTrueValue();
            var b2 = DummyCreator.GetFalseValue();

            Expect.That(() => b1 && b2);

            var assertBuilder = AssertBuilderFactory.GetAssertBuilder();
            A.CallTo(() => assertBuilder.GetIsTrueAction(A<Expression>._)).MustHaveHappened(Repeated.Exactly.Twice);
        }

        [Test]
        public void That_HaveThreeBooleanInsideAssertBlock_BothConditionsAreTested()
        {
            AssertBuilderFactoryForTests.FakeAssertBuilder();            


            var b1 = DummyCreator.GetTrueBooleanValue();
            var b2 = DummyCreator.GetTrueBooleanValue();
            var b3 = DummyCreator.GetTrueBooleanValue();

            Expect.That(() => b1 && b2 && b3);

            var assertBuilder = AssertBuilderFactory.GetAssertBuilder();
            A.CallTo(() => assertBuilder.GetIsTrueAction(A<Expression>._)).MustHaveHappened(Repeated.Exactly.Times(3));
        }
    }
}
