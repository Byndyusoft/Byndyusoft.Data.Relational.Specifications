using System;
using System.Collections.Generic;
using System.Linq;

namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class AndSpecification : Specification
    {
        private readonly Specification[] _inner;

        private AndSpecification(Specification[] specs)
            : base(Clause(specs))
        {
            _inner = specs;

            Array.ForEach(specs, spec => AddParams(spec.Params));
        }

        private static string Clause(Specification[] specs)
        {
            var parts = specs.Select(spec => $"({spec})");
            return string.Join(" AND ", parts);
        }

        internal static Specification Create(params Specification[] specifications)
        {
            var notEmpty = specifications.Where(x => x.IsEmpty == false).ToArray();
            if (notEmpty.Any() == false)
                return Empty;

            var notTrue = notEmpty.Where(x => x.IsTrue == false).ToArray();
            if (notTrue.Length == 0)
                return True;

            if (notTrue.Any(x => x.IsFalse))
                return False;

            if (notTrue.Length == 1)
                return notTrue[0];

            var specs = new List<Specification>();
            foreach (var spec in notTrue)
            {
                if (spec is AndSpecification and)
                {
                    specs.AddRange(and._inner);
                    continue;
                }

                specs.Add(spec);
            }

            return new AndSpecification(specs.ToArray());
        }

        private bool Equals(AndSpecification other)
        {
            return other._inner.Length == _inner.Length && _inner.All(spec => other._inner.Any(x => Equals(spec, x)));
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is AndSpecification other && Equals(other);
        }

        public override int GetHashCode() => 0;
    }
}