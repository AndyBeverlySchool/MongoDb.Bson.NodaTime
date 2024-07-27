using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class DurationSerializer : PatternSerializer<Duration>
    {
        public DurationSerializer(DurationPattern pattern) : base(pattern)
        {
        }
    }
}