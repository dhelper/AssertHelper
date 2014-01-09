using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeMock.ArrangeActAssert;

namespace AssertHelper.Core.Tests
{
    [TestFixture, Isolated]
    public class AssertTestsCollections
    {
        [Test]
        public void This_CheckIfArrayConatainsElement_CollectionAssertContainsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection = new[] { 1, 2, 3, 4, 5 };

            Assert.This(() => collection.Contains(3));

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.Contains(collection, 3));
        }

        [Test]
        public void This_CheckIfListConatainsElement_CollectionAssertContainsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection = new List<int> { 1, 2, 3, 4, 5 };

            Assert.This(() => collection.Contains(3));

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.Contains(collection, 3));
        }

        [Test]
        public void This_CheckIfHashSetConatainsElement_CollectionAssertContainsCalled()
        {
            Isolate.Fake.StaticMethods(typeof(NUnit.Framework.CollectionAssert));

            var collection = new HashSet<int> { 1, 2, 3, 4, 5 };

            Assert.This(() => collection.Contains(3));

            Isolate.Verify.WasCalledWithExactArguments(() => CollectionAssert.Contains(collection, 3));
        }
    }
}
