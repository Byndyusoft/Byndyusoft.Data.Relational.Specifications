namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class TrueSpecification : Specification
    {
        public TrueSpecification() : base("1=1")
        {
        }

        protected internal override bool IsTrue => true;

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is TrueSpecification;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}