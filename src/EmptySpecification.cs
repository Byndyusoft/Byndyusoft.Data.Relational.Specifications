namespace Byndyusoft.Data.Relational.Specifications
{
    internal class EmptySpecification : Specification
    {
        public EmptySpecification() : base("1=1")
        {
        }

        protected internal override bool IsEmpty => true;

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is EmptySpecification;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}