using System;
using System.Collections.Generic;
using System.Linq;

namespace Byndyusoft.Data.Relational.Specifications
{
    internal sealed class OrSpecification : Specification
    {
        private readonly Specification[] _inner;

        private OrSpecification(Specification[] specs)
            : base(Clause(specs))
        {
            _inner = specs;

            Array.ForEach(specs, spec => AddParams(spec.Params));
        }

        private static string Clause(Specification[] specs)
        {
            var parts = specs.Select(spec => $"({spec})");
            return string.Join(" OR ", parts);
        }

        internal static Specification Create(params Specification[] specifications)
        {
            var notEmpty = specifications.Where(x => x.IsEmpty == false).ToArray();
            if (notEmpty.Any() == false)
                return Empty;

            var notFalse = notEmpty.Where(x => x.IsFalse == false).ToArray();
            if (notFalse.Length == 0)
                return False;

            if (notFalse.Any(x => x.IsTrue))
                return True;

            if (notFalse.Length == 1)
                return notFalse[0];

            var specs = new List<Specification>();
            foreach (var spec in notFalse)
            {
                if (spec is OrSpecification or)
                {
                    specs.AddRange(or._inner);
                    continue;
                }

                specs.Add(spec);
            }

            return new OrSpecification(specs.ToArray());
        }

        private bool Equals(OrSpecification other)
        {
            return other._inner.Length == _inner.Length && _inner.All(spec => other._inner.Any(x => Equals(spec, x)));
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is OrSpecification other && Equals(other);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}