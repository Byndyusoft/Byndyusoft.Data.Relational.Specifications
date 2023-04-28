namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class NotSpecification : Specification
    {
        private readonly Specification _inner;

        private NotSpecification(Specification inner)
            : base(Clause(inner))
        {
            _inner = inner;
            AddParams(inner.Params);
        }

        private static string Clause(Specification inner)
        {
            return $"NOT ({inner})";
        }

        private bool Equals(NotSpecification other)
        {
            return Equals(_inner, other._inner);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is NotSpecification other && Equals(other);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        internal static Specification Create(Specification inner)
        {
            if (inner.IsFalse)
                return True;
            if (inner.IsTrue)
                return False;
            if (inner.IsEmpty)
                return Empty;
            if (inner is NotSpecification not)
                return not._inner;

            return new NotSpecification(inner);
        }
    }
}