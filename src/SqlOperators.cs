using System;
using System.Collections.Generic;

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

            var sql = $"{column} ILIKE @{column}";

            var param = new Dictionary<string, object?> {{column, $"%{value}%"}};
            return Specification.Create(sql, param);
        }

        public Specification Like(string column, string value)
        {
            if (string.IsNullOrWhiteSpace(column)) throw new ArgumentNullException(nameof(column));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            var sql = $"{column} LIKE @{column}";

            var param = new Dictionary<string, object?> {{column, $"%{value}%"}};
            return Specification.Create(sql, param);
        }

        public Specification IsNull(string column)
        {
            if (string.IsNullOrWhiteSpace(column)) throw new ArgumentNullException(nameof(column));

            return Specification.Create($"{column} IS NULL");
        }

        public Specification IsNotNull(string column)
        {
            if (string.IsNullOrWhiteSpace(column)) throw new ArgumentNullException(nameof(column));

            return Specification.Create($"{column} IS NOT NULL");
        }

        public Specification Eq<T>(string column, T value)
        {
            if (string.IsNullOrWhiteSpace(column)) throw new ArgumentNullException(nameof(column));

            var sql = $"{column} = @{column}";
            var param = new Dictionary<string, object?> {{column, value}};

            return Specification.Create(sql, param);
        }

        public Specification Any<T>(string column, params T[] array)
        {
            if (string.IsNullOrWhiteSpace(column)) throw new ArgumentNullException(nameof(column));

            var sql = $"{column} = ANY(@{column})";
            var param = new Dictionary<string, object?> {{column, array}};

            return Specification.Create(sql, param);
        }
    }
}