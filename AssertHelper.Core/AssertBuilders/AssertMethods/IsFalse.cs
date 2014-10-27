using System;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class IsFalse : UnaryAssertMethodBase
    {
        public IsFalse(Type assertType)
            : base(assertType, "IsFalse", new[] { typeof(bool) })
        { }
    }
}