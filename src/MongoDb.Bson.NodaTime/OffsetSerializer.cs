using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class OffsetSerializer : PatternSerializer<Offset>
    {
        public OffsetSerializer() : base(OffsetPattern.GeneralInvariant)
        { }
    }
}
