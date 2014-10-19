using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsCollections : FakeAssertBuilderTests
    {
        [Test]
        public void That_CheckIfArrayConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionContains(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection.Contains(7));

            validator.WasAssertCalledWithArguments(collection, 7);
        }

        [Test]
        public void That_CheckIfArrayConatainsElementInVariable_CollectionAssertContainsCalled()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };
            var val = 7;

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionContains(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection.Contains(val));

            validator.WasAssertCalledWithArguments(collection, val);
        }

        [Test]
        public void That_CheckIfListConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new List<int> { 1, 2, 3, 4, 5 };

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionContains(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection.Contains(7));

            validator.WasAssertCalledWithArguments(collection, 7);
        }

        [Test]
        public void That_CheckIfHashSetConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new HashSet<int> { 1, 2, 3, 4, 5 };

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionContains(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection.Contains(7));

            validator.WasAssertCalledWithArguments(collection, 7);
        }

        [Test]
        public void That_CheckIfArrayEqualsToArray_CollectionEqualsCalled()
        {
            var collection1 = new [] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 6 };

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionEquals(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection1 == collection2);

            validator.WasAssertCalledWithArguments(collection2, collection1);
        }

        [Test]
        public void That_CheckIfListEqualsToList_CollectionEqualsCalled()
        {
            var collection1 = new List<int> { 1, 2, 3, 4, 5 };
            var collection2 = new List<int> { 1, 2, 3, 4, 6 };

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionEquals(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection1 == collection2);

            validator.WasAssertCalledWithArguments(collection2, collection1);
        }

        [Test]
        public void That_CheckIfArrayNotEqualsToArray_CollectionNotEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 5 };

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionNotEquals(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection1 != collection2);

            validator.WasAssertCalledWithArguments(collection2, collection1);
        }

        [Test]
        public void That_CheckIfListNotEqualsToList_CollectionNotEqualsCalled()
        {
            var collection1 = new List<int> { 1, 2, 3, 4, 5 };
            var collection2 = new List<int> { 1, 2, 3, 4, 5 };

            var fakeBuilder = AssertBuilderFactoryForTests.FakeAssertBuilder();

            var validator = new CallValidator();
            A.CallTo(() => fakeBuilder.GetCollectionNotEquals(A<Expression>._, A<Expression>._)).AddAssertValidation(validator);

            Expect.That(() => collection1 != collection2);

            validator.WasAssertCalledWithArguments(collection2, collection1);
        }
    }
}
