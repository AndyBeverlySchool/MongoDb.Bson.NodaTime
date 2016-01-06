using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NodaTime;

namespace MongoDb.Bson.NodaTime
{
    public class InstantSerializer : SerializerBase<Instant>
    {
        public override Instant Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.GetCurrentBsonType();
            switch (type)
            {
                case BsonType.Int64:
                    return Instant.FromTicksSinceUnixEpoch(context.Reader.ReadInt64());
                case BsonType.String:
                    return Instant.FromTicksSinceUnixEpoch(long.Parse(context.Reader.ReadString()));
                default:
                    throw new NotSupportedException($"Cannot convert a {type} to an Instant.");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Instant value)
        {
            context.Writer.WriteInt64(value.Ticks);
        }
    }
}
