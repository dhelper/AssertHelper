using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssertHelper.Core.AssertBuilders.NUnit
{
    internal class AssertMethods
    {
        private readonly MethodInfo _isFalse;
        private readonly MethodInfo _isTrue;
        private readonly MethodInfo _isNull;
        private readonly MethodInfo _isNotNull;
        private readonly MethodInfo _areEqual;
        private readonly MethodInfo _areNotEqual;
        private readonly MethodInfo _isInstanceOfType;
        private readonly MethodInfo _fail;

        public AssertMethods(IEnumerable<Type> nunitTypes)
        {
            var assertType = nunitTypes.Single(t => t.Name == "Assert");

            _isFalse = assertType.GetMethod("IsFalse", new[] { typeof(bool) });
            _isTrue = assertType.GetMethod("IsTrue", new[] { typeof(bool) });
            _isNull = assertType.GetMethod("IsNull", new[] { typeof(object) });
            _isNotNull = assertType.GetMethod("IsNotNull", new[] { typeof(object) });
            _areEqual = assertType.GetMethod("AreEqual", new[] { typeof(object), typeof(object) });
            _areNotEqual = assertType.GetMethod("AreNotEqual", new[] { typeof(object), typeof(object) });
            _isInstanceOfType = assertType.GetMethod("IsInstanceOf", new[] { typeof(Type), typeof(object) });
            _fail = assertType.GetMethod("Fail", new[] { typeof(string) });
        }

        public MethodInfo IsFalse { get { return _isFalse; } }
        public MethodInfo IsTrue { get { return _isTrue; } }
        public MethodInfo IsNull { get { return _isNull; } }
        public MethodInfo IsNotNull { get { return _isNotNull; } }
        public MethodInfo AreEqual { get { return _areEqual; } }
        public MethodInfo AreNotEqual { get { return _areNotEqual; } }
        public MethodInfo IsInstanceOfType { get { return _isInstanceOfType; } }
        public MethodInfo Fail { get { return _fail; } }
    }
}
