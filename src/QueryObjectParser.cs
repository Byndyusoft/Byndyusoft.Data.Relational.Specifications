using System;
using System.Dynamic;
using System.Threading;

namespace Byndyusoft.Data.Relational.Specifications
{
    internal static class QueryObjectParser
    {
        private static int _counter;

        public static (string, ExpandoObject?) Parse(FormattableString formattableString)
        {
            if (formattableString == null) throw new ArgumentNullException(nameof(formattableString));

            if (formattableString.ArgumentCount == 0) return (formattableString.ToString(), null);

            var parameters = new ExpandoObject();
            var arguments = new object[formattableString.ArgumentCount];
            for (var i = 0; i < formattableString.ArgumentCount; i++)
            {
                arguments[i] = GenerateArgumentName("arg");
                parameters.Add((string) arguments[i], formattableString.GetArgument(i));
            }

            var sql = string.Format(formattableString.Format, arguments);
            return (sql, parameters);
        }

        private static string GenerateArgumentName(string prefix)
        {
            return $"@{prefix}_{Interlocked.Increment(ref _counter)}";
        }
    }
}