using System;
using System.Linq.Expressions;

namespace BuilderImmutableObject
{
    public static class Builder
    {
        public static ObjectBuilder<TObject> Set<TObject, TValue>(this TObject obj, Expression<Func<TObject, TValue>> propertyAcessorFunc, TValue value)
        {
            return new ObjectBuilder<TObject>(obj).Set(propertyAcessorFunc, value);
        }
    }
}