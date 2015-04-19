using System;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders.AssertMethods
{
    internal class IsNotNull : UnaryAssertMethodBase
    {
        public IsNotNull(Type assertType)
            : base(assertType, "IsNotNull", new[] { typeof(object), typeof(string) })
        { }
    }
}