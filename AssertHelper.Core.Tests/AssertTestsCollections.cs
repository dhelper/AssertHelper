using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AssertHelper.Core.Tests
{
    [TestFixture]
    public class AssertTestsCollections
    {
        [Test]
        public void That_CheckIfArrayConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection.Contains(7)));

            StringAssert.Contains("Expected: collection containing 7", result.Message);
            StringAssert.Contains("But was:  < 1, 2, 3, 4, 5 >", result.Message);
        }

        [Test]
        public void That_CheckIfArrayConatainsElementInVariable_CollectionAssertContainsCalled()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };
            var val = 7;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection.Contains(val)));

            StringAssert.Contains("Expected: collection containing 7", result.Message);
            StringAssert.Contains("But was:  < 1, 2, 3, 4, 5 >", result.Message);
        }

        [Test]
        public void That_CheckIfListConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new List<int> { 1, 2, 3, 4, 5 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection.Contains(7)));

            StringAssert.Contains("Expected: collection containing 7", result.Message);
            StringAssert.Contains("But was:  < 1, 2, 3, 4, 5 >", result.Message);
        }

        [Test]
        public void That_CheckIfHashSetConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new HashSet<int> { 1, 2, 3, 4, 5 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection.Contains(7)));

            StringAssert.Contains("Expected: collection containing 7", result.Message);
            StringAssert.Contains("But was:  < 1, 2, 3, 4, 5 >", result.Message);
        }

        [Test]
        public void That_CheckIfArrayEqualsToArray_CollectionEqualsCalled()
        {
            var collection1 = new [] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 6 };
            
            var result = Assert.Throws<AssertionException>(() =>Expect.That(() => collection1 == collection2));

            var expected = AssertTestBase.GetAssertionMessage(() => CollectionAssert.AreEqual(collection2, collection1));

            Assert.That(result.Message, Is.EqualTo(expected));
        }

        [Test]
        public void That_CheckIfListEqualsToList_CollectionEqualsCalled()
        {
            var collection1 = new List<int> { 1, 2, 3, 4, 5 };
            var collection2 = new List<int> { 1, 2, 3, 4, 6 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection1 == collection2));

            var expected = AssertTestBase.GetAssertionMessage(() => CollectionAssert.AreEqual(collection2, collection1));

            Assert.That(result.Message, Is.EqualTo(expected));
        }

        [Test]
        public void That_CheckIfArrayNotEqualsToArray_CollectionNotEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 5 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection1 != collection2));

            var expected = AssertTestBase.GetAssertionMessage(() => CollectionAssert.AreNotEqual(collection2, collection1));

            Assert.That(result.Message, Is.EqualTo(expected));
        }

        [Test]
        public void That_CheckIfListNotEqualsToList_CollectionNotEqualsCalled()
        {
            var collection1 = new List<int> { 1, 2, 3, 4, 5 };
            var collection2 = new List<int> { 1, 2, 3, 4, 5 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection1 != collection2));

            var expected = AssertTestBase.GetAssertionMessage(() => CollectionAssert.AreNotEqual(collection2, collection1));

            Assert.That(result.Message, Is.EqualTo(expected));
        }
    }
}
