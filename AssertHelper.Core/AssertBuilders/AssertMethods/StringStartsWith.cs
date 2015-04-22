using System;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class StringStartsWith : BinaryAssertMethodBase
    {
        public StringStartsWith(Type assertType)
            : base(assertType, "StartsWith", new[] { typeof(string), typeof(string), typeof(string) })
        { }
    }
}