using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class OffsetSerializer : PatternSerializer<Offset>
    {
        /// <inheritdoc />
        public OffsetSerializer(OffsetPattern pattern) : base(pattern)
        {
        }
    }
}