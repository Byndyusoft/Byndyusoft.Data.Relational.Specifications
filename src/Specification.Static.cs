using System;
using System.Linq;

namespace Byndyusoft.Data.Relational.Specifications
{
    public partial class Specification
    {
        public static Specification Empty { get; } = new EmptySpecification();

        public static Specification True { get; } = new TrueSpecification();

        public static Specification False { get; } = new FalseSpecification();

        public static SqlOperators Ops { get; } = new SqlOperators();

        public static Specification CreateFormat(FormattableString formattableString)
        {
            var (sql, param) = QueryObjectParser.Parse(formattableString);
            return new Specification(sql, param);
        }

        public static Specification Create(string sql, object? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            return new Specification(sql, parameters);
        }

        public static Specification And(params Specification[] specifications)
        {
            if (specifications == null) throw new ArgumentNullException(nameof(specifications));

            return specifications.Aggregate(Empty, (current, specification) => current & specification);
        }

        public static Specification Or(params Specification[] specifications)
        {
            if (specifications == null) throw new ArgumentNullException(nameof(specifications));

            return specifications.Aggregate(Empty, (current, specification) => current | specification);
        }
    }
}