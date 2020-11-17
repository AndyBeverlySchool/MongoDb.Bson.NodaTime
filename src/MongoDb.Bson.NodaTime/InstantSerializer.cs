using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class InstantSerializer : PatternSerializer<Instant>
    {
        public InstantSerializer() 
            : base(InstantPattern.ExtendedIso)
        {
        }
    }
}
