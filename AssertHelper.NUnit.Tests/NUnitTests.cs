using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using AssertHelper.TestBase;

namespace AssertHelper.NUnit.Tests
{
    [TestFixture]
    public class NUnitTests : FrameworkSpecificAssertTestsBase
    {
        protected override string CreateExpectedMessage(string expectedLambda, AssertionException expectedException)
        {
            return expectedLambda + expectedException.Message;
        }

        protected override Action<int, int> AssertEqualAction
        {
            get { return Assert.AreEqual; }
        }

        protected override Action<int, int> AssertNotEqualAction
        {
            get { return Assert.AreNotEqual; }
        }

        protected override Action<object> AssertIsNullAction
        {
            get { return Assert.IsNull; }
        }

        protected override Action<object> AssertIsNotNullAction
        {
            get { return Assert.IsNotNull; }
        }

        protected override Action<bool> AssertIsTrueAction
        {
            get { return Assert.IsTrue; }
        }

        protected override Action<bool> AssertIsFalseAction
        {
            get { return Assert.IsFalse; }
        }

        protected override Action<ICollection, object> AssertCollectionContainsAction
        {
            get { return CollectionAssert.Contains; }
        }

        protected override Action<ICollection, ICollection> AssertCollectionEqualsAction
        {
            get { return CollectionAssert.AreEqual; }
        }
        protected override Action<ICollection, ICollection> AssertCollectionNotEqualsAction
        {
            get { return CollectionAssert.AreNotEqual; }
        }

        protected override Action<Type, object> AssertInstanceOfTypeAction
        {
            get { return Assert.IsInstanceOf; }
        }

        protected override Action<string, string> AssertStringContainsAction
        {
            get { return StringAssert.Contains; }
        }

        protected override Action<string, string> AssertStringStartsWithAction
        {
            get { return StringAssert.StartsWith; }
        }

        protected override Action<string, string> AssertStringEndsWithAction
        {
            get { return StringAssert.EndsWith; }
        }
    }
}
