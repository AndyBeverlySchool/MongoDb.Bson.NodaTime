using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class LocalDateSerializer : PatternSerializerBase<LocalDate>
    {
        public LocalDateSerializer() : base(LocalDatePattern.IsoPattern, d => d.WithCalendar(CalendarSystem.Iso))
        {}
    }
}