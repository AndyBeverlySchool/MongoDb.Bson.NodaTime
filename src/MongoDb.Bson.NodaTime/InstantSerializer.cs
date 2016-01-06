using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class InstantSerializer : PatternSerializerBase<Instant>
    {
        public InstantSerializer() : base(InstantPattern.ExtendedIsoPattern)
        {}
    }
}
