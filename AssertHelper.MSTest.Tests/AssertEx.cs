using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertHelper.MSTest.Tests
{
    public static class AssertEx
    {
        public static T Throws<T>(Action action) where T : Exception
        {
            try
            {
                action();

                Assert.Fail("Expected exception of type {0}", typeof(T));
            }
            catch (T exc)
            {
                return exc;
            }

            return null;
        }
    }
}
