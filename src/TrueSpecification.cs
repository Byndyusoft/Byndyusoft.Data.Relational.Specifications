using System;

namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class TrueSpecification : Specification
    {
        public TrueSpecification() : base(string.Empty)
        {
        }

        protected internal override bool IsTrue { get; } = true;

        protected override Specification AndCore(Specification right)
        {
            return right.IsEmpty ? this : right;
        }

        protected override Specification OrCore(Specification right)
        {
            return right.IsFalse ? right : this;
        }

        protected override Specification NotCore()
        {
            return False;
        }
    }
}