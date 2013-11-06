﻿using System;

namespace AssertHelper.Core.Tests
{
    public static class DummyCreator
    {
        public static bool GetBooleanValue()
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

        public static object GetReferenceObject()
        {
            return new object();
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