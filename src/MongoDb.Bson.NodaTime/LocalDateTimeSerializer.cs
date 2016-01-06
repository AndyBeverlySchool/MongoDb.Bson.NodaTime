using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class LocalDateTimeSerializer : PatternSerializerBase<LocalDateTime>
    {
        public LocalDateTimeSerializer() : base(LocalDateTimePattern.ExtendedIsoPattern, d => d.WithCalendar(CalendarSystem.Iso))
        {}
    }
}