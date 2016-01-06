using System;
using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace MongoDb.Bson.NodaTime
{
    public static class Serializers
    {
        public static void Register()
        {
            var classes = typeof(Serializers).GetTypeInfo().Assembly.DefinedTypes
                .Where(t => t.BaseType != null && !t.ContainsGenericParameters && t.ImplementedInterfaces.Contains(typeof(IBsonSerializer)))
                .ToList();
            classes.ForEach(t => BsonSerializer.RegisterSerializer(t.BaseType.GenericTypeArguments[0], Activator.CreateInstance(t.AsType()) as IBsonSerializer));
        }
    }
}