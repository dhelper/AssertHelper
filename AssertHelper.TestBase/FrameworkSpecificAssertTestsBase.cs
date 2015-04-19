using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private static void AssertIgnoreWhitespace(string expected, string actual)
        {
            Assert.AreEqual(Regex.Replace(expected, @"\s+", String.Empty), Regex.Replace(actual, @"\s+", String.Empty));
        }

        private static string GetExpectedMessage(TestDelegate action, string expectedLambda)
        {
            var expectedException = AssertEx.Throws<AssertionException>(action);
            var expectedMessage = "(" + expectedLambda + ")" + expectedException.Message;
            return expectedMessage;
        }

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

            var expectedMessage = GetExpectedMessage(() => AssertEqualAction(expected, actual), "actual == expected");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfTwoValuesAreDifferent_ReturnSameErrorMessageAsAreNotEqual()
        {
            int actual = 1;
            int expected = 1;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => actual != expected));

            var expectedMessage = GetExpectedMessage(() => AssertNotEqualAction(expected, actual), "actual != expected");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToTrue_ReturnSameErrorMessageAsIsTrue()
        {
            bool actual = false;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => actual == true));

            var expectedMessage = GetExpectedMessage(() => AssertIsTrueAction(actual), "actual == True");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToFalse_ReturnSameErrorMessageAsIsFalse()
        {
            bool actual = true;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => actual == false));

            var expectedMessage = GetExpectedMessage(() => AssertIsFalseAction(actual), "actual == False");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayConatainsElement_CollectionAssertContainsCalled()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => collection.Contains(7)));

            var expectedMessage = GetExpectedMessage(() => AssertCollectionContainsAction(collection, 7), "collection.Contains(7)");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayEqualsToArray_CollectionEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 6 };

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => collection1 == collection2));

            var expectedMessage = GetExpectedMessage(() => AssertCollectionEqualsAction(collection2, collection1), "collection1 == collection2");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CheckIfArrayNotEqualsToArray_CollectionNotEqualsCalled()
        {
            var collection1 = new[] { 1, 2, 3, 4, 5 };
            var collection2 = new[] { 1, 2, 3, 4, 5 };

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => collection1 != collection2));

            var expectedMessage = GetExpectedMessage(() => AssertCollectionNotEqualsAction(collection2, collection1), "collection1 != collection2");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_UseIsToCheckType_AssertIsInstaceOfCalled()
        {
            var val = new object();

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => val is string));

            var expectedMessage = GetExpectedMessage(() => AssertInstanceOfTypeAction(typeof(string), val), "val is string");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueToNull_AssertIsNullCalled()
        {
            var val = new object();

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => val == null));

            var expectedMessage = GetExpectedMessage(() => AssertIsNullAction(val), "val == null");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_CompareValueNotEqualToNull_AssertIsNotNullCalled()
        {
            object val = null;

            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => val != null));

            var expectedMessage = GetExpectedMessage(() => AssertIsNotNullAction(val), "val != null");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringContainsCalled_StringAssertContainedUsed()
        {
            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => "1234".Contains("5")));

            var expectedMessage = GetExpectedMessage(() => AssertStringContainsAction("5", "1234"), "\"1234\".Contains(\"5\")");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringStartsWithCalled_StringAssertStartsWithUsed()
        {
            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => "1234".StartsWith("2")));

            var expectedMessage = GetExpectedMessage(() => AssertStringStartsWithAction("2", "1234"), "\"1234\".StartsWith(\"2\")");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }

        [Test]
        public void That_StringEndssWithCalled_StringAssertStartsWithUsed()
        {
            var result = AssertEx.Throws<AssertionException>(() => Expect.That(() => "1234".EndsWith("2")));

            var expectedMessage = GetExpectedMessage(() => AssertStringEndsWithAction("2", "1234"), "\"1234\".EndsWith(\"2\")");

            AssertIgnoreWhitespace(expectedMessage, result.Message);
        }
    }
}