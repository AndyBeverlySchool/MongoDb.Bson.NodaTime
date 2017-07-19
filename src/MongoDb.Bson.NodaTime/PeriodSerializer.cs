using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class PeriodSerializer : PatternSerializer<Period>
    {
        public PeriodSerializer() : base(PeriodPattern.NormalizingIso)
        {}
    }
}