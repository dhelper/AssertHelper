using System;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class StringEndsWith : BinaryAssertMethodBase
    {
        public StringEndsWith(Type assertType)
            : base(assertType, "EndsWith", new[] { typeof(string), typeof(string) })
        { }
    }
}