using System;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class StringContains : BinaryAssertMethodBase
    {
        public StringContains(Type assertType)
            : base(assertType, "Contains", new[] { typeof(string), typeof(string), typeof(string) })
        { }
    }
}