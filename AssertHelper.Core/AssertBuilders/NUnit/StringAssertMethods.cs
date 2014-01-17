using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssertHelper.Core.AssertBuilders.NUnit
{
    internal class StringAssertMethods
    {
        private readonly MethodInfo _contains;
        private readonly MethodInfo _startsWith;
        private readonly MethodInfo _endsWith;

        public StringAssertMethods(IEnumerable<Type> nunitTypes)
        {
            var stringAssertType = nunitTypes.Single(type => type.Name == "StringAssert");
            _contains = stringAssertType.GetMethod("Contains", new[] { typeof(string), typeof(string) });
            _startsWith = stringAssertType.GetMethod("StartsWith", new[] { typeof(string), typeof(string) });
            _endsWith = stringAssertType.GetMethod("EndsWith", new[] { typeof(string), typeof(string) });
        }

        public MethodInfo Contains { get { return _contains; } }
        public MethodInfo StartsWith { get { return _startsWith; } }
        public MethodInfo EndsWith { get { return _endsWith; } }
    }
}
