using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsCollections
    {
        [Test]
        public void That_CheckIfArrayConatainsElement_CollectionAssertContainsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection = new[] { 1, 2, 3, 4, 5 };

            Expect.That(() => collection.Contains(3));

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.Contains(collection, 3));
        }

        [Test]
        public void That_CheckIfArrayConatainsElementInVariable_CollectionAssertContainsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection = new[] { 1, 2, 3, 4, 5 };
            var val = 3;
            Expect.That(() => collection.Contains(val));

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.Contains(collection, 3));
        }

        [Test]
        public void That_CheckIfListConatainsElement_CollectionAssertContainsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection = new List<int> { 1, 2, 3, 4, 5 };

            Expect.That(() => collection.Contains(3));

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.Contains(collection, 3));
        }

        [Test]
        public void That_CheckIfHashSetConatainsElement_CollectionAssertContainsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection = new HashSet<int> { 1, 2, 3, 4, 5 };

            Expect.That(() => collection.Contains(3));

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.Contains(collection, 3));
        }

        [Test]
        public void That_CheckIfArrayEqualsToArray_CollectionEqualsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection1 = new [] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 5 };
            
            Expect.That(() => collection1 == collection2);

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.AreEqual(collection2, collection1));
        }

        [Test]
        public void That_CheckIfListEqualsToList_CollectionEqualsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(CollectionAssert));

            var collection1 = new List<int> { 1, 2, 3, 4, 5 };
            var collection2 = new List<int> { 1, 2, 3, 4, 5 };

            Expect.That(() => collection1 == collection2);

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.AreEqual(collection2, collection1));
        }

        [Test]
        public void That_CheckIfArrayNotEqualsToArray_CollectionNotEqualsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(CollectionAssert));

            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 5 };

            Expect.That(() => collection1 != collection2);

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.AreNotEqual(collection2, collection1));
        }

        [Test]
        public void That_CheckIfListNotEqualsToList_CollectionNotEqualsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection1 = new List<int> { 1, 2, 3, 4, 5 };
            var collection2 = new List<int> { 1, 2, 3, 4, 5 };

            Expect.That(() => collection1 != collection2);

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.AreNotEqual(collection2, collection1));
        }
    }
}
