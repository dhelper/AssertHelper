using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace AssertHelper.NUnit.Tests
{
    [TestFixture]
    public class NUnitTests : FrameworkSpecificAssertTestsBase
    {
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

        protected override Action<IEnumerable<T>, T> AssertCollectionContainsAction<T>()
        {
            return (collection, item) => CollectionAssert.Contains(collection, item);
        }

        protected override Action<IEnumerable<T>, IEnumerable<T>> AssertCollectionEqualsAction<T>()
        {
            return CollectionAssert.AreEqual;
        }
        protected override Action<IEnumerable<T>, IEnumerable<T>> AssertCollectionNotEqualsAction<T>()
        {
            return CollectionAssert.AreNotEqual;
        }

        protected override Action<Type, object> AssertInstanceOfTypeAction
        {
            get
            {
                return Assert.IsInstanceOf;
            }
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
