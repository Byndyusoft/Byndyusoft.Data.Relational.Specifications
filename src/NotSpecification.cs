namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class NotSpecification : Specification
    {
        internal NotSpecification(Specification inner)
            : base(Clause(inner))
        {
            Inner = inner;
            Add(inner.Params);
        }

        public Specification Inner { get; }

        public override Specification Not()
        {
            return Inner;
        }

        private static string Clause(Specification inner)
        {
            return $"NOT ({inner})";
        }
    }
}