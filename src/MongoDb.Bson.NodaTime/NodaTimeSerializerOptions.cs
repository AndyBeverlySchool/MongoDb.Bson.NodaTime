using NodaTime;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public sealed class NodaTimeSerializerOptions
    {
        public NodaTimeSerializerOptions WithCalendarSystem(CalendarSystem pattern)
        {
            CalendarSystem = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithDurationPattern(DurationPattern pattern)
        {
            DurationPattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithInstantPattern(InstantPattern pattern)
        {
            InstantPattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithLocalDatePattern(LocalDatePattern pattern)
        {
            LocalDatePattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithLocalTimePattern(LocalTimePattern pattern)
        {
            LocalTimePattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithLocalDateTimePattern(LocalDateTimePattern pattern)
        {
            LocalDateTimePattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithOffsetDateTimePattern(OffsetDateTimePattern pattern)
        {
            OffsetDateTimePattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithOffsetPattern(OffsetPattern pattern)
        {
            OffsetPattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithPeriodPattern(PeriodPattern pattern)
        {
            PeriodPattern = pattern;
            return this;
        }

        public NodaTimeSerializerOptions WithZonedDateTimePattern(ZonedDateTimePattern pattern)
        {
            ZonedDateTimePattern = pattern;
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
        public IPattern<ZonedDateTime> ZonedDateTimePattern { get; private set; } = global::NodaTime.Text.ZonedDateTimePattern.CreateWithInvariantCulture("G", DateTimeZoneProviders.Tzdb);
    }
}