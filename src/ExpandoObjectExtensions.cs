using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Byndyusoft.Data.Relational.Specifications
{
    internal static class ExpandoObjectExtensions
    {
        public static void Add(this ExpandoObject expandoObject, string key, object value)
        {
            IDictionary<string, object> dic = expandoObject;
            dic.Add(key, value);
        }

        public static void Add(this ExpandoObject expandoObject, IDictionary<string, object>? param)
        {
            if (param == null)
                return;

            foreach (var pair in param) expandoObject.Add(pair.Key, pair.Value);
        }

        public static void Add(this ExpandoObject expandoObject, object? param)
        {
            if (param == null)
                return;

            if (param is IDictionary<string, object> dic)
            {
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