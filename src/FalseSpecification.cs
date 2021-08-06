namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class FalseSpecification : Specification
    {
        public FalseSpecification() : base("1<>1")
        {
        }

        protected internal override bool IsFalse { get; } = true;

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is FalseSpecification;
        }

        public override int GetHashCode()
        {
            return IsFalse.GetHashCode();
        }
    }
}