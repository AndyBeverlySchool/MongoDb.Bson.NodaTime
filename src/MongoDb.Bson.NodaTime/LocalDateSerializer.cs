using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class LocalDateSerializer : PatternSerializer<LocalDate>
    {
        public LocalDateSerializer(LocalDatePattern pattern, CalendarSystem calendarSystem)
            : base(pattern, d => d.WithCalendar(calendarSystem))
        {
        }
    }
}