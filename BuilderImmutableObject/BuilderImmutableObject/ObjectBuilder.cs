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

        private static string GetPropertyName(Expression expression)
        {
            return ((MemberExpression)expression).Member.Name;
        }

        public TObject Build()
        {
            var properties = GetProperties();

            _newInstance = (TObject)Activator.CreateInstance(typeof(TObject), true);

            foreach (var propertyInfo in properties.Where(x => !_selectedProperties.ContainsKey(x.Name)))
            {
                if(propertyInfo.CanWrite) propertyInfo.SetValue
                (
                    obj: _newInstance,
                    value: GetValueObject(propertyInfo)
                );
            }

            foreach (var property in _selectedProperties)
            {
                var propertyInfo = _newInstance.GetType().GetProperty(property.Key);

                if(propertyInfo.CanWrite) propertyInfo.SetValue(_newInstance, property.Value);
            }

            return _newInstance;
        }

        private static IEnumerable<PropertyInfo> GetProperties()
        {
            return typeof(TObject).GetProperties();
        }

        private object GetValueObject(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetValue(_obj);
        }
    }
}