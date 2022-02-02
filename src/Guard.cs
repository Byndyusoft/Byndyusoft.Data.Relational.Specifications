using System;

namespace Byndyusoft.Data.Relational.Specifications
{
    internal static class Guard
    {
        public static T NotNull<T>(T value, string paramName)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException(paramName);
            return value;
        }

        public static string IsNotNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(paramName);
            return value;
        }
    }
}