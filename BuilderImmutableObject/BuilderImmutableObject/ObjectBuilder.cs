using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BuilderImmutableObject
{
    public class ObjectBuilder<TObject>
    {
        private readonly TObject _obj;
        private TObject _newInstance;
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
        {
            return ((MemberExpression)expression).Member.Name;
        }

        public TObject Build()
        {
            var propertiesNames = GetPropertiesName();

            _newInstance = Activator.CreateInstance<TObject>();

            foreach (var propertyInfo in propertiesNames)
            {
                var value = _selectedProperties.ContainsKey(propertyInfo.Name)
                    ? _selectedProperties[propertyInfo.Name]
                    : default(object);

                propertyInfo.SetValue
                (
                    obj: _newInstance,
                    value: value ?? GetValueObject(propertyInfo)
                );
            }

            return _newInstance;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesName()
        {
            return typeof(TObject).GetProperties();
        }

        private object GetValueObject(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetValue(_obj);
        }
    }
}
