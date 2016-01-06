using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class LocalDateSerializer : SerializerBase<LocalDate>
    {
        public override LocalDate Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.GetCurrentBsonType();
            switch (type)
            {
                case BsonType.Array:
                    context.Reader.ReadStartArray();
                    var calendarId = context.Reader.ReadString();
                    var localDate = LocalDatePattern.IsoPattern.CheckedParse(context.Reader.ReadString());
                    context.Reader.ReadEndArray();

                    return localDate.WithCalendar(CalendarSystem.ForId(calendarId));
                case BsonType.String:
                    return LocalDatePattern.IsoPattern.CheckedParse(context.Reader.ReadString());
                default:
                    throw new NotSupportedException($"Cannot convert a {type} to a LocalDate.");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, LocalDate value)
        {
            var pattern = LocalDatePattern.IsoPattern;

            if (value.Calendar != CalendarSystem.Iso)
            {
                context.Writer.WriteStartArray();
                context.Writer.WriteString(value.Calendar.Id);
                context.Writer.WriteString(pattern.Format(value));
                context.Writer.WriteEndArray();
            }
            else
            {
                context.Writer.WriteString(pattern.Format(value));
            }
        }
    }
}