using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsInstance : FakeAssertBuilderTests
    {
        [Test]
        public void That_UseIsToCheckType_AssertIsInstaceOfCalled()
        {
            var val = DummyCreator.GetDummy();

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetIsInstanceOf(A<Type>._, A<Expression>._, A<string>._)).AddAssertValidation(validator);

            Expect.That(() => val is string);

            validator.WasAssertCalledWithArguments(typeof(string), val);
        } 
    }
}
