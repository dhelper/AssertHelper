using System;

namespace AssertHelper.Core.Tests
{
    public static class DummyCreator
    {
        private static readonly object _referenceObject1 = new object();
        private static readonly object _referenceObject2 = new object();

        public static bool GetTrueBooleanValue()
        {
            return true;
        }

        public static bool GetTrueValue()
        {
            return true;
        }

        public static bool GetFalseValue()
        {
            return false;
        }

        public static object GetReferenceObject1()
        {
            return _referenceObject1;
        }

        public static object GetReferenceObject2()
        {
            return _referenceObject2;
        }

        public static object GetDummy()
        {
            return new DummyClass();
        }

        public static string GetString()
        {
            return Guid.NewGuid().ToString();
        }

        public static int GetInt()
        {
            return 0;
        }

        public static double GetDouble()
        {
            return 0;
        }
    }

    public class DummyClass
    {
        
    }
}