using System;

namespace Byndyusoft.Data.Relational.Specifications
{
    public partial class Specification
    {
        public static Specification Empty { get; } = new EmptySpecification();

        public static Specification True { get; } = new TrueSpecification();

        public static Specification False { get; } = new FalseSpecification();

        public static SqlOperators Ops { get; } = new SqlOperators();

        public static Specification Create(string sql, object? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            return new Specification(sql, parameters);
        }

        public static Specification And(params Specification[] specifications)
        {
            if (specifications == null) throw new ArgumentNullException(nameof(specifications));

            return AndSpecification.Create(specifications);
        }

        public static Specification Or(params Specification[] specifications)
        {
            if (specifications == null) throw new ArgumentNullException(nameof(specifications));

            return OrSpecification.Create(specifications);
        }

        public static Specification Not(Specification inner)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));

            return NotSpecification.Create(inner);
        }

        public static Specification operator &(Specification left, Specification right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return And(left, right);
        }

        public static Specification operator |(Specification left, Specification right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return Or(left, right);
        }

        public static Specification operator !(Specification specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(Specification));

            return Not(specification);
        }

        public static bool operator ==(Specification? left, Specification? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Specification? left, Specification? right)
        {
            return Equals(left, right) == false;
        }
    }
}