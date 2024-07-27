using System;
using MongoDB.Bson.Serialization;

namespace MongoDb.Bson.NodaTime
{
    public static class NodaTimeSerializers
    {
        public static void Register() => RegisterInternal(null);
        public static void Register(Func<NodaTimeSerializerOptions> options) => RegisterInternal(options?.Invoke());

        private static void RegisterInternal(NodaTimeSerializerOptions options)
        {
            options = options ?? new NodaTimeSerializerOptions();
            BsonSerializer.RegisterSerializer(new DurationSerializer(options.DurationPattern));
            BsonSerializer.RegisterSerializer(new InstantSerializer(options.InstantPattern));
            BsonSerializer.RegisterSerializer(new LocalDateSerializer(options.LocalDatePattern, options.CalendarSystem));
            BsonSerializer.RegisterSerializer(new LocalDateTimeSerializer(options.LocalDateTimePattern, options.CalendarSystem));
            BsonSerializer.RegisterSerializer(new LocalTimeSerializer(options.LocalTimePattern));
            BsonSerializer.RegisterSerializer(new OffsetDateTimeSerializer(options.OffsetDateTimePattern, options.CalendarSystem));
            BsonSerializer.RegisterSerializer(new OffsetSerializer(options.OffsetPattern));
            BsonSerializer.RegisterSerializer(new PeriodSerializer(options.PeriodPattern));
            BsonSerializer.RegisterSerializer(new ZonedDateTimeSerializer(options.ZonedDateTimePattern));
        }
    }
}