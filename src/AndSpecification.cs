namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class AndSpecification : Specification
    {
        internal AndSpecification(Specification left, Specification right)
            : base(Clause(left, right))
        {
            Add(left.Params);
            Add(right.Params);
        }

        private static string Clause(Specification left, Specification right)
        {
            return $"({left}) AND ({right})";
        }
    }
}