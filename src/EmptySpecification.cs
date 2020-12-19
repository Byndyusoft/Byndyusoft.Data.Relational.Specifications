namespace Byndyusoft.Data.Relational.Specifications
{
    internal class EmptySpecification : Specification
    {
        public EmptySpecification() : base(string.Empty)
        {
        }

        protected internal override bool IsEmpty { get; } = true;

        protected override Specification AndCore(Specification right)
        {
            return right;
        }

        protected override Specification OrCore(Specification right)
        {
            return right;
        }

        protected override Specification NotCore()
        {
            return this;
        }
    }
}