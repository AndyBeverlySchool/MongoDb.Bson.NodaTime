using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class ZonedDateTimeSerializer : PatternSerializer<ZonedDateTime>
    {
        private static readonly IPattern<ZonedDateTime> Pattern = ZonedDateTimePattern.CreateWithInvariantCulture("G", DateTimeZoneProviders.Tzdb);

        public ZonedDateTimeSerializer(IPattern<ZonedDateTime> pattern) : base(pattern)
        {
        }
    }
}