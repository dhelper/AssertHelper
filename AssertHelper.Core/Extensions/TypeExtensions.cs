using System;

namespace AssertHelper.Core.Extensions
{
    internal static class TypeExtensions 
    {
        public static bool IsEnumerableType(this Type type)
        {
            return type.GetInterface("IEnumerable") != null;
        }

        public static bool Is<T>(this Type type)
        {
            return type == typeof(T);
        }

        public static bool IsNot<T>(this Type type)
        {
            return type != typeof(T);
        }
    }
}
