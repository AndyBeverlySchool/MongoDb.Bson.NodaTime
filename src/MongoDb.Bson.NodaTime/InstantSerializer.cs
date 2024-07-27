using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class InstantSerializer : PatternSerializer<Instant>
    {
        public InstantSerializer(InstantPattern pattern) : base(pattern)
        {
        }

        public static bool UseExtendedIsoStringPattern { get; set; } = false;

        public override Instant Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (UseExtendedIsoStringPattern) return base.Deserialize(context, args);
            var type = context.Reader.GetCurrentBsonType();
            switch (type)
            {
                case BsonType.DateTime:
                    return Instant.FromUnixTimeMilliseconds(context.Reader.ReadDateTime());
                case BsonType.String:
                    return InstantPattern.ExtendedIso.CheckedParse(context.Reader.ReadString());
                case BsonType.Null:
                    throw new InvalidOperationException("Instant is a value type, but the BsonValue is null.");
                default:
                    throw new NotSupportedException($"Cannot convert a {type} to an Instant.");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Instant value)
        {
            if (UseExtendedIsoStringPattern)
            {
                base.Serialize(context, args, value);
            }
            else
            {
                context.Writer.WriteDateTime(value.ToUnixTimeTicks() / NodaConstants.TicksPerMillisecond);
            }
        }
    }
}