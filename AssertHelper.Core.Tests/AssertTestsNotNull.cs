using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsNotNull : FakeAssertBuilderTests
    {
       [Test]
        public void That_CompareValueNotEqualToNull_AssertNotNullCalled()
        {
            var val = new object();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsNotNullAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => val != null);

            validator.WasAssertCalledWithArguments(val);
        }

       [Test]
       public void That_NullableBoolAndPassNotEqualToNull_FinishNormally()
       {
           var value = (bool?)DummyCreator.GetTrueValue();

           Expect.That(() => value != null);
       }

        [Test]
        public void That_CompareValueNotEqualToNull_AssertIsNullCalled()
        {
            object val = null;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsNotNullAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => val != null);

            validator.WasAssertCalledWithArguments(val);
        }

        [Test]
        public void That_CompareValueNotEqualToNullReversed_AssertIsNullCalled()
        {
            object val = null;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsNotNullAction(A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => null != val);

            validator.WasAssertCalledWithArguments(val);
        }
    }
}