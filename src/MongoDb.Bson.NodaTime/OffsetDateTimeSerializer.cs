using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class OffsetDateTimeSerializer : PatternSerializer<OffsetDateTime>
    {
        /// <inheritdoc />
        public OffsetDateTimeSerializer(OffsetDateTimePattern pattern, CalendarSystem calendarSystem) : base(pattern, d => d.WithCalendar(calendarSystem))
        {
        }
    }
}