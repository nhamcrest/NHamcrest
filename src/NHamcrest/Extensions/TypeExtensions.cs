using System;
using System.Linq;
using System.Reflection;

namespace NHamcrest.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsSimpleType(this Type type)
        {
            return
                type.GetTypeInfo().IsValueType ||
                type.GetTypeInfo().IsPrimitive ||
                new[] {
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
            }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}