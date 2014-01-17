using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssertHelper.Core.AssertBuilders.NUnit
{
    internal class CollectionAssertMethods
    {
        private readonly MethodInfo _contains;
        private readonly MethodInfo _areEqual;
        private readonly MethodInfo _areNotEqual;
        private IEnumerable<Type> nunitTypes;

        public CollectionAssertMethods(IEnumerable<Type> nunitTypes)
        {
            var collectionAssertType = nunitTypes.Single(type => type.Name == "CollectionAssert");
            _contains = collectionAssertType.GetMethod("Contains", new[] { typeof(IEnumerable), typeof(object) });
            _areEqual = collectionAssertType.GetMethod("AreEqual", new[] { typeof(IEnumerable), typeof(IEnumerable) });
            _areNotEqual = collectionAssertType.GetMethod("AreNotEqual", new[] { typeof(IEnumerable), typeof(IEnumerable) });
        }

        public MethodInfo Contains { get { return _contains; } }
        public MethodInfo AreEqual { get { return _areEqual; } }
        public MethodInfo AreNotEqual { get { return _areNotEqual; } }
    }
}
