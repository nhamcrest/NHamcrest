using System;
using System.Linq;

namespace NHamcrest.Core
{
    internal static class TypeExtensions
    {
        public static bool IsSimpleType(
            this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new[] { 
				typeof(String),
				typeof(Decimal),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(Guid)
			}.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}
