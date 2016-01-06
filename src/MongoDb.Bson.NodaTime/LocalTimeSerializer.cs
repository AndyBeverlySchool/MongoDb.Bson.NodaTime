using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class LocalTimeSerializer : PatternSerializerBase<LocalTime>
    {
        public LocalTimeSerializer() : base(LocalTimePattern.ExtendedIsoPattern)
        {}
    }
}