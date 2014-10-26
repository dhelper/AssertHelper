using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssertHelper.Core;
#if NUNIT
using NUnit.Framework;
using AssertEx = NUnit.Framework.Assert;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using AssertHelper.MSTest.Tests;
using AssertionException = Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException;
#endif

namespace AssertHelper.TestBase
{
    public abstract class FrameworkSpecificAssertTestsBase
    {
        protected abstract Action<int, int> AssertEqualAction { get; }
        protected abstract Action<int, int> AssertNotEqualAction { get; }
        protected abstract Action<object> AssertIsNullAction { get; }
        protected abstract Action<object> AssertIsNotNullAction { get; }
        protected abstract Action<bool> AssertIsTrueAction { get; }
        protected abstract Action<bool> AssertIsFalseAction { get; }
        protected abstract Action<ICollection, object> AssertCollectionContainsAction { get; }
        protected abstract Action<ICollection, ICollection> AssertCollectionEqualsAction { get; }
        protected abstract Action<ICollection, ICollection> AssertCollectionNotEqualsAction { get; }
        protected abstract Action<Type, object> AssertInstanceOfTypeAction { get; }
        protected abstract Action<string, string> AssertStringContainsAction { get; }
        protected abstract Action<string, string> AssertStringStartsWithAction { get; }
        protected abstract Action<string, string> AssertStringEndsWithAction { get; }

        [Test]
        public void That_CompareTwoNumbers_ReturnSameErrorMessageAsAreEqual()
        {
            int actual = 1;
            int expected = 2;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => actual == expected));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertEqualAction(expected, actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfTwoValuesAreDifferent_ReturnSameErrorMessageAsAreNotEqual()
        {
            int actual = 1;
            int expected = 1;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => actual != expected));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertNotEqualAction(expected, actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToTrue_ReturnSameErrorMessageAsIsTrue()
        {
            bool actual = false;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => actual == true));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertIsTrueAction(actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToFalse_ReturnSameErrorMessageAsIsFalse()
        {
            bool actual = true;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => actual == false));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertIsFalseAction(actual)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => collection.Contains(7)));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertCollectionContainsAction(collection, 7)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayEqualsToArray_CollectionEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 6 };

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => collection1 == collection2));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertCollectionEqualsAction(collection2, collection1)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayNotEqualsToArray_CollectionNotEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 5 };

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => collection1 != collection2));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertCollectionNotEqualsAction(collection2, collection1)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_UseIsToCheckType_AssertIsInstaceOfCalled()
        {
            var val = new object();

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => val is string));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertInstanceOfTypeAction(typeof(string), val)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToNull_AssertIsNullCalled()
        {
            var val = new object();

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => val == null));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertIsNullAction(val)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueNotEqualToNull_AssertIsNotNullCalled()
        {
            object val = null;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => val != null));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertIsNotNullAction(val)).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringContainsCalled_StringAssertContainedUsed()
        {
            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => "1234".Contains("5")));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertStringContainsAction("5", "1234")).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringStartsWithCalled_StringAssertStartsWithUsed()
        {
            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => "1234".StartsWith("2")));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertStringStartsWithAction("2", "1234")).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringEndssWithCalled_StringAssertStartsWithUsed()
        {
            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => "1234".EndsWith("2")));

            var expectedMessage = AssertEx.Throws<AssertionException>(() => AssertStringEndsWithAction("2", "1234")).Message;

            Assert.AreEqual(expectedMessage, result.Message);
        }
    }
}