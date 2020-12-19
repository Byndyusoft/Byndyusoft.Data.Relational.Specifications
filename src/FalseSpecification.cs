namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class FalseSpecification : Specification
    {
        public FalseSpecification() : base("1<>1")
        {
        }

        protected internal override bool IsFalse { get; } = true;

        protected override Specification AndCore(Specification right)
        {
            return this;
        }

        protected override Specification OrCore(Specification right)
        {
            return right.IsTrue || right.IsEmpty ? this : right;
        }

        protected override Specification NotCore()
        {
            return True;
        }
    }
}