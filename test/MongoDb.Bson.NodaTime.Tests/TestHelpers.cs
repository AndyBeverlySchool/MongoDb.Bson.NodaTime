using MongoDB.Bson;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MongoDb.Bson.NodaTime.Tests
{
    internal static class TestHelpers
    {
        public static string ToTestJson<TType>(this TType obj)
        {
            return obj.ToJson().Replace('"', '\'');
        }
    }
}
