using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class LocalTimeSerializer : PatternSerializer<LocalTime>
    {
        public LocalTimeSerializer(LocalTimePattern pattern) : base(pattern)
        {
        }
    }
}