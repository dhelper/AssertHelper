using System;
using System.Collections.Generic;
using System.Linq;
using AssertHelper.Core;
using NUnit.Framework;

namespace AssertHelper.NUnit.Tests
{
    public abstract class FrameworkSpecificAssertTestsBase
    {
        protected abstract Action<int, int> AssertEqualAction { get; }
        protected abstract Action<int, int> AssertNotEqualAction { get; }
        protected abstract Action<object> AssertIsNullAction { get; }
        protected abstract Action<object> AssertIsNotNullAction { get; }
        protected abstract Action<bool> AssertIsTrueAction { get; }
        protected abstract Action<bool> AssertIsFalseAction { get; }
        protected abstract Action<IEnumerable<T>, T> AssertCollectionContainsAction<T>();
        protected abstract Action<IEnumerable<T>, IEnumerable<T>> AssertCollectionEqualsAction<T>();
        protected abstract Action<IEnumerable<T>, IEnumerable<T>> AssertCollectionNotEqualsAction<T>();
        protected abstract Action<Type, object> AssertInstanceOfTypeAction { get; }
        protected abstract Action<string, string> AssertStringContainsAction { get; }
        protected abstract Action<string, string> AssertStringStartsWithAction { get; }
        protected abstract Action<string, string> AssertStringEndsWithAction { get; }

        [Test]
        public void That_CompareTwoNumbers_ReturnSameErrorMessageAsAreEqual()
        {
            int actual = 1;
            int expected = 2;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == expected));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertEqualAction(expected, actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfTwoValuesAreDifferent_ReturnSameErrorMessageAsAreNotEqual()
        {
            int actual = 1;
            int expected = 1;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual != expected));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertNotEqualAction(expected, actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToTrue_ReturnSameErrorMessageAsIsTrue()
        {
            bool actual = false;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == true));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertIsTrueAction(actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToFalse_ReturnSameErrorMessageAsIsFalse()
        {
            bool actual = true;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => actual == false));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertIsFalseAction(actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection.Contains(7)));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertCollectionContainsAction<int>()(collection, 7)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayEqualsToArray_CollectionEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 6 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection1 == collection2));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertCollectionEqualsAction<int>()(collection2, collection1)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayNotEqualsToArray_CollectionNotEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 5 };

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => collection1 != collection2));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertCollectionNotEqualsAction<int>()(collection2, collection1)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_UseIsToCheckType_AssertIsInstaceOfCalled()
        {
            var val = new object();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => val is string));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertInstanceOfTypeAction(typeof(string), val)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToNull_AssertIsNullCalled()
        {
            var val = new object();

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => val == null));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertIsNullAction(val)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueNotEqualToNull_AssertIsNotNullCalled()
        {
            object val = null;

            var result = Assert.Throws<AssertionException>(() => Expect.That(() => val != null));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertIsNotNullAction(val)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringContainsCalled_StringAssertContainedUsed()
        {
            var result = Assert.Throws<AssertionException>(() => Expect.That(() => "1234".Contains("5")));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertStringContainsAction("5", "1234")).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringStartsWithCalled_StringAssertStartsWithUsed()
        {
            var result = Assert.Throws<AssertionException>(() => Expect.That(() => "1234".StartsWith("2")));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertStringStartsWithAction("2", "1234")).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringEndssWithCalled_StringAssertStartsWithUsed()
        {
            var result = Assert.Throws<AssertionException>(() => Expect.That(() => "1234".EndsWith("2")));

            var expectedMessage = Assert.Throws<AssertionException>(() => AssertStringEndsWithAction("2", "1234")).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}