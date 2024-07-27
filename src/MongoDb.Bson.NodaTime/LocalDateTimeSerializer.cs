using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class LocalDateTimeSerializer : PatternSerializer<LocalDateTime>
    {
        public LocalDateTimeSerializer(LocalDateTimePattern pattern, CalendarSystem calendarSystem) : base(pattern, d => d.WithCalendar(calendarSystem))
        {
        }
    }
}