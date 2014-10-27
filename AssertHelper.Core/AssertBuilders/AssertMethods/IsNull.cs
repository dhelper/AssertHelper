using System;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class IsNull : UnaryAssertMethodBase
    {
        public IsNull(Type assertType)
            : base(assertType, "IsNull", new[] { typeof(object) })
        { }
    }
}