using System;
using System.Dynamic;

namespace Byndyusoft.Data.Relational.Specifications
{
    public partial class Specification
    {
        private ExpandoObject? _params;

        protected Specification()
        {
        }

        internal Specification(string sql, object? param = null) : this()
        {
            Sql = sql ?? throw new ArgumentNullException(nameof(sql));
            AddParams(param);
        }

        protected internal virtual bool IsEmpty => false;
        protected internal virtual bool IsTrue => false;
        protected internal virtual bool IsFalse => false;

        public string Sql { get; } = default!;

        public object? Params => _params;

        public Specification Not()
        {
            return Not(this);
        }

        public Specification And(Specification right)
        {
            if (right == null) throw new ArgumentNullException(nameof(right));

            return And(this, right);
        }

        public Specification And(string sql, object? param = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            return And(new Specification(sql, param));
        }

        public Specification Or(Specification right)
        {
            if (right == null) throw new ArgumentNullException(nameof(right));

            return Or(this, right);
        }

        public Specification Or(string sql, object? param = null)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            return Or(new Specification(sql, param));
        }

        public override string ToString()
        {
            return Sql;
        }

        protected void AddParams(object? param)
        {
            if (param == null)
                return;

            (_params ??= new ExpandoObject()).Add(param);
        }

        private bool Equals(Specification other)
        {
            return string.Equals(Sql, other.Sql) && ExpandoObjectExtensions.Equals(_params, other._params);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Specification) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}