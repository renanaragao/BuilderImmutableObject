using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BuilderImmutableObject
{
    public class ObjectBuilder<TObject>
    {
        private readonly TObject _obj;
        private readonly IDictionary<string, object> _selectedProperties;

        public ObjectBuilder(TObject obj)
        {
            _obj = obj;
            _selectedProperties = new Dictionary<string, object>();
        }

        public ObjectBuilder<TObject> Set<TValue>(Expression<Func<TObject, TValue>> propertyAcessorFunc, TValue value)
        {
            _selectedProperties.Add(GetPropertyName(propertyAcessorFunc.Body), value);

            return this;
        }

        private static string GetPropertyName(Expression expression)
        => ((MemberExpression)expression).Member.Name;

        public TObject Build()
        {
            var type = typeof(TObject);

            Span<PropertyInfo> properties = GetProperties(type);

            var newInstance = Activator.CreateInstance(type, true);

            for (int i = 0; i < properties.Length; i++)
            {
                object value;
                if(!_selectedProperties.TryGetValue(properties[i].Name, out value))
                    value = properties[i].GetValue(_obj);

                properties[i].SetValue
                (
                    obj: newInstance,
                    value: value
                );
            }

            return (TObject)newInstance;
        }

        private static PropertyInfo[] GetProperties(Type type)
            => type
                .GetProperties()
                .Where(x => x.CanRead && x.CanWrite)
                .ToArray();
    }
}
