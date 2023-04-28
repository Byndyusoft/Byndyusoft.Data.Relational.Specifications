using System.Collections.Generic;
using CommunityToolkit.Diagnostics;

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
            Guard.IsNotNullOrWhiteSpace(column, nameof(column));
            Guard.IsNotNullOrWhiteSpace(value, nameof(value));

            var sql = $"{column} ILIKE @{column}";

            var param = new Dictionary<string, object?> { { column, $"%{value}%" } };
            return Specification.Create(sql, param);
        }

        public Specification Like(string column, string value)
        {
            Guard.IsNotNullOrWhiteSpace(column, nameof(column));
            Guard.IsNotNullOrWhiteSpace(value, nameof(value));

            var sql = $"{column} LIKE @{column}";

            var param = new Dictionary<string, object?> { { column, $"%{value}%" } };
            return Specification.Create(sql, param);
        }

        public Specification IsNull(string column)
        {
            Guard.IsNotNullOrWhiteSpace(column, nameof(column));

            return Specification.Create($"{column} IS NULL");
        }

        public Specification IsNotNull(string column)
        {
            Guard.IsNotNullOrWhiteSpace(column, nameof(column));

            return Specification.Create($"{column} IS NOT NULL");
        }

        public Specification Eq<T>(string column, T value)
        {
            Guard.IsNotNullOrWhiteSpace(column, nameof(column));

            var sql = $"{column} = @{column}";
            var param = new Dictionary<string, object?> { { column, value } };

            return Specification.Create(sql, param);
        }

        public Specification Any<T>(string column, params T[] array)
        {
            Guard.IsNotNullOrWhiteSpace(column, nameof(column));

            var sql = $"{column} = ANY(@{column})";
            var param = new Dictionary<string, object?> { { column, array } };

            return Specification.Create(sql, param);
        }
    }
}