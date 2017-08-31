using System;
using System.Collections.Generic;
using System.Linq;
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

        private object GetValueObject(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetValue(_obj);
        }

        private static IEnumerable<PropertyInfo> GetPropertiesName()
        {
            return typeof(TObject).GetProperties();
        }

        private static string GetPropertyName(Expression expression)
        {
            return ((MemberExpression)expression).Member.Name;
        }

        public TObject Build()
        {
            var propertiesNames = GetPropertiesName();

            _newInstance = Activator.CreateInstance<TObject>();

            foreach (var propertyInfo in propertiesNames.Where(x => !_selectedProperties.ContainsKey(x.Name)))
            {
                propertyInfo.SetValue
                (
                    obj: _newInstance,
                    value: Convert.ChangeType(GetValueObject(propertyInfo), propertyInfo.PropertyType)
                );
            }

            foreach (var property in _selectedProperties)
            {
                _newInstance.GetType().GetProperty(property.Key).SetValue(_newInstance, property.Value);
            }

            return _newInstance;
        }
    }
}