using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Byndyusoft.Data.Relational.Specifications
{
    internal static class ExpandoObjectExtensions
    {
        public static bool Equals(ExpandoObject? obj1, ExpandoObject? obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            var dic1 = obj1 as IDictionary<string, object>;
            var dic2 = obj2 as IDictionary<string, object>;

            if (dic1 == null && dic2 != null)
                return false;
            if (dic1 != null && dic2 == null)
                return false;

            return dic1!.Count == dic2!.Count && dic1.Except(dic2).Any() == false;
        }

        private static void Add(this ExpandoObject expandoObject, string key, object value)
        {
            IDictionary<string, object> dic = expandoObject;
            dic.Add(key, value);
        }

        private static void Add(this ExpandoObject expandoObject, IEnumerable<KeyValuePair<string, object>>? param)
        {
            if (param == null)
                return;

            foreach (var pair in param) expandoObject.Add(pair.Key, pair.Value);
        }

        public static void Add(this ExpandoObject expandoObject, object? param)
        {
            switch (param)
            {
                case null:
                    return;
                case IEnumerable<KeyValuePair<string, object>> dic:
                    expandoObject.Add(dic);
                    return;
            }

            var properties = param.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(param, null);
                expandoObject.Add(name, value);
            }
        }
    }
}