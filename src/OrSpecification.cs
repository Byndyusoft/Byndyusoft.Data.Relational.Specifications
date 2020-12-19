namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class OrSpecification : Specification
    {
        internal OrSpecification(Specification left, Specification right)
            : base(Clause(left, right))
        {
            Add(left.Params);
            Add(right.Params);
        }

        private static string Clause(Specification left, Specification right)
        {
            return $"({left}) OR ({right})";
        }
    }
}