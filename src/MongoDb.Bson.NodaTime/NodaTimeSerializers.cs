using System;
using MongoDB.Bson.Serialization;
using NodaTime;
using NodaTime.Text;
using NodaZonedDateTimePattern = NodaTime.Text.ZonedDateTimePattern;
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

    public sealed class NodaTimeSerializerOptions
    {
        public NodaTimeSerializerOptions WithCalendarSystem(CalendarSystem calendarSystem)
        {
            CalendarSystem = calendarSystem;
            return this;
        }

        public NodaTimeSerializerOptions WithDurationPattern(DurationPattern durationPattern)
        {
            DurationPattern = durationPattern;
            return this;
        }

        public CalendarSystem CalendarSystem { get; private set; } = CalendarSystem.Iso;
        public DurationPattern DurationPattern { get; private set; } = DurationPattern.Roundtrip;
        public InstantPattern InstantPattern { get; private set; } = InstantPattern.ExtendedIso;
        public LocalDatePattern LocalDatePattern { get; private set; } = LocalDatePattern.Iso;
        public LocalTimePattern LocalTimePattern { get; private set; } = LocalTimePattern.ExtendedIso;
        public LocalDateTimePattern LocalDateTimePattern { get; private set; } = LocalDateTimePattern.ExtendedIso;
        public OffsetDateTimePattern OffsetDateTimePattern { get; private set; } = OffsetDateTimePattern.ExtendedIso;
        public OffsetPattern OffsetPattern { get; private set; } = OffsetPattern.GeneralInvariant;
        public PeriodPattern PeriodPattern { get; private set; } = PeriodPattern.NormalizingIso;
        public IPattern<ZonedDateTime> ZonedDateTimePattern { get; private set; } = NodaZonedDateTimePattern.CreateWithInvariantCulture("G", DateTimeZoneProviders.Tzdb);
    }
}