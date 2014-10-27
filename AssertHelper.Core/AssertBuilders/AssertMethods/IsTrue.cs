using System;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class IsTrue : UnaryAssertMethodBase
    {
        public IsTrue(Type assertType)
            : base(assertType, "IsTrue", new[] { typeof(bool) })
        { }
    }
}