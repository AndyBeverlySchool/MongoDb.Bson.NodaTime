using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    internal static class PatternExtensions
    {
        public static TResult CheckedParse<TResult>(this IPattern<TResult> pattern, string candidate)
        {
            var value = pattern.Parse(candidate);
            if (!value.Success)
            {
                throw value.Exception;
            }

            return value.Value;
        }
    }
}