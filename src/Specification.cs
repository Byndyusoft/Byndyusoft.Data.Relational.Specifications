using System;
using System.Dynamic;

namespace Byndyusoft.Data.Relational.Specifications
{
    public partial class Specification : IQueryObject
    {
        private ExpandoObject? _params;

        protected Specification()
        {
        }

        internal Specification(string sql, object? param = null) : this()
        {
            Sql = sql ?? throw new ArgumentNullException(nameof(sql));
            Add(param);
        }

        protected internal virtual bool IsEmpty { get; } = false;
        protected internal virtual bool IsTrue { get; } = false;
        protected internal virtual bool IsFalse { get; } = false;

        public string Sql { get; } = default!;

        public object? Params => _params;

        public virtual Specification Not()
        {
            return NotCore();
        }

        public Specification And(Specification right)
        {
            if (right == null) throw new ArgumentNullException(nameof(right));

            return AndCore(right);
        }

        public Specification And(string sql, object? param = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            return AndCore(new Specification(sql, param));
        }

        public Specification Or(Specification right)
        {
            if (right == null) throw new ArgumentNullException(nameof(right));

            return OrCore(right);
        }

        public Specification Or(string sql, object? param = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            return OrCore(new Specification(sql, param));
        }

        public override string ToString()
        {
            return Sql;
        }

        public static Specification operator &(Specification left, Specification right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return left.And(right);
        }

        public static Specification operator |(Specification left, Specification right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            return left.Or(right);
        }

        public static Specification operator !(Specification specification)
        {
            if (specification == null) throw new ArgumentNullException(nameof(Specification));

            return specification.Not();
        }

        protected virtual Specification AndCore(Specification right)
        {
            return new AndSpecification(this, right);
        }

        protected virtual Specification OrCore(Specification right)
        {
           return new OrSpecification(this, right);
        }

        protected virtual Specification NotCore()
        {
            return new NotSpecification(this);
        }

        protected void Add(object? param)
        {
            if (param == null)
                return;

            (_params ??= new ExpandoObject()).Add(param);
        }
    }
}