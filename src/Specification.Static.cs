using CommunityToolkit.Diagnostics;

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
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            return new Specification(sql, parameters);
        }

        public static Specification And(params Specification[] specifications)
        {
            Guard.IsNotNull(specifications, nameof(specifications));

            return AndSpecification.Create(specifications);
        }

        public static Specification Or(params Specification[] specifications)
        {
            Guard.IsNotNull(specifications, nameof(specifications));

            return OrSpecification.Create(specifications);
        }

        public static Specification Not(Specification inner)
        {
            Guard.IsNotNull(inner, nameof(inner));

            return NotSpecification.Create(inner);
        }

        public static Specification operator &(Specification left, Specification right)
        {
            Guard.IsNotNull(left, nameof(left));
            Guard.IsNotNull(right, nameof(right));

            return And(left, right);
        }

        public static Specification operator |(Specification left, Specification right)
        {
            Guard.IsNotNull(left, nameof(left));
            Guard.IsNotNull(right, nameof(right));

            return Or(left, right);
        }

        public static Specification operator !(Specification specification)
        {
            Guard.IsNotNull(specification, nameof(specification));

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