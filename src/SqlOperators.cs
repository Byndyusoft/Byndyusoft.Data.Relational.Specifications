using System;
using System.Runtime.CompilerServices;

namespace Byndyusoft.Data.Relational.Specifications
{
    public class SqlOperators
    {
        internal SqlOperators()
        {
        }

        // ReSharper disable once InconsistentNaming
        public Specification ILike(string column, string value)
        {
            if (string.IsNullOrWhiteSpace(column)) throw new ArgumentNullException(nameof(column));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            
            return Op(column, "ILIKE", $"%{value}%");
        }

        public Specification Eq<T>(string column, T value)
        {
            if (string.IsNullOrWhiteSpace(column)) throw new ArgumentNullException(nameof(column));

            return Op(column, "=", value);
        }

        private Specification Op<T>(string column, string op, T value)
        {
            var queryObject = FormattableStringFactory.Create($"{column} {op} {{0}}", value);
            return Specification.CreateFormat(queryObject);
        }

        public Specification Any<T>(string column, params T[] array)
        {
            var queryObject = FormattableStringFactory.Create($"{column} = ANY({{0}})", array);
            return Specification.CreateFormat(queryObject);
        }
    }
}