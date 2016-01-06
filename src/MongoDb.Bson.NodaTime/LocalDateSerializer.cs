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
                    var localDate = ParseDatePattern(context.Reader.ReadString());
                    context.Reader.ReadEndArray();

                    return localDate.WithCalendar(CalendarSystem.ForId(calendarId));
                case BsonType.String:
                    return ParseDatePattern(context.Reader.ReadString());
                default:
                    throw new NotSupportedException($"Cannot convert a {type} to a LocalDate.");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, LocalDate value)
        {
            if (value.Calendar != CalendarSystem.Iso)
            {
                context.Writer.WriteStartArray();
                context.Writer.WriteString(value.Calendar.Id);
                context.Writer.WriteString(LocalDatePattern.IsoPattern.Format(value));
                context.Writer.WriteEndArray();
            }
            else
            {
                context.Writer.WriteString(LocalDatePattern.IsoPattern.Format(value));
            }
        }

        private static LocalDate ParseDatePattern(string datePattern)
        {
            var localDate = LocalDatePattern.IsoPattern.Parse(datePattern);
            if (!localDate.Success)
            {
                throw localDate.Exception;
            }

            return localDate.Value;
        }
    }
}