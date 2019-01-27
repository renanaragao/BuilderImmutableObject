using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;

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
        {
            return ((MemberExpression)expression).Member.Name;
        }

        public TObject Build()
            => Lambda<Func<TObject>>(Block(GetChangeProperties())).Compile()();

        private IEnumerable<Expression> GetChangeProperties()
        {
            var typeObject = typeof(TObject);

            var newInstance = New(typeObject);

            var members = GetMembers(typeObject);

            var entity = Parameter(typeObject, "entity");

            foreach (var member in members)
            {
                var value = _selectedProperties.ContainsKey(member.Name)
                    ? _selectedProperties[member.Name]
                    : GetValueObject((PropertyInfo)member);

                var property = PropertyOrField(entity, member.Name);
                var assign = Assign(property, Constant(value));
                var lambda = Lambda<Func<TObject, object>>(assign, entity);

                yield return SetProperty(lambda);
            }
        }

        private static IEnumerable<MemberInfo> GetMembers(Type type)
            => type.GetMembers(BindingFlags.GetProperty |
                               BindingFlags.GetField |
                               BindingFlags.SetField |
                               BindingFlags.SetProperty);

        private object GetValueObject(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetValue(_obj);
        }

        public static Expression<Action<TEntity, TValue>> SetProperty<TEntity,
            TValue>(Expression<Func<TEntity, TValue>> propertyGetExpression)
        {
            var entityParameterExpression =
                (ParameterExpression)((MemberExpression)propertyGetExpression.Body).Expression;
            var valueParameterExpression = Parameter(typeof(TValue));

            return Lambda<Action<TEntity, TValue>>(
                Assign(propertyGetExpression.Body, valueParameterExpression),
                entityParameterExpression,
                valueParameterExpression);
        }
    }
}
