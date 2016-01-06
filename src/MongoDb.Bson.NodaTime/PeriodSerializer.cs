using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class PeriodSerializer : PatternSerializerBase<Period>
    {
        public PeriodSerializer() : base(PeriodPattern.NormalizingIsoPattern)
        {}
    }
}