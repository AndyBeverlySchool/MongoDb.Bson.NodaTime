using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using Xunit;
using FluentAssertions;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class ZonedDateTimeSerializerTests
    {
        private readonly static DateTimeZone easternTimezone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("America/New_York");
        static ZonedDateTimeSerializerTests()
        {
            var options = new NodaTimeSerializerOptions();
            BsonSerializer.RegisterSerializer(new ZonedDateTimeSerializer(options.ZonedDateTimePattern));
        }

        [Fact]
        public void CanRoundTripValue_Eastern()
        {
            var dateTime = new ZonedDateTime(new LocalDateTime(2015, 1, 2, 3, 4, 5).InUtc().ToInstant(), easternTimezone);
            var obj = new Test { ZonedDateTime = dateTime };
            obj.ToTestJson().Should().Contain("'ZonedDateTime' : '2015-01-01T22:04:05 America/New_York (-05)'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.ZonedDateTime.Should().Be(obj.ZonedDateTime);
            deserialized.ZonedDateTime.Should().Be(dateTime);
            deserialized.ZonedDateTime.Zone.Should().Be(easternTimezone);
        }

        [Fact]
        public void CanRoundTripValue_UTC()
        {
            var dateTime = new ZonedDateTime(new LocalDateTime(2015, 1, 2, 3, 4, 5).InUtc().ToInstant(), DateTimeZone.Utc);
            var obj = new Test { ZonedDateTime = dateTime };
            obj.ToTestJson().Should().Contain("'ZonedDateTime' : '2015-01-02T03:04:05 UTC (+00)'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.ZonedDateTime.Should().Be(obj.ZonedDateTime);
            deserialized.ZonedDateTime.Should().Be(dateTime);
            deserialized.ZonedDateTime.Zone.Should().Be(DateTimeZone.Utc);
        }

        [Fact]
        public void ThrowsWhenDateIsInvalid()
        {
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("ZonedDateTime", "bleh"))));
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("ZonedDateTime", BsonNull.Value))));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("NullableZonedDateTime", BsonNull.Value))).NullableZonedDateTime.Should().BeNull();
        }

        private class Test
        {
            public ZonedDateTime ZonedDateTime { get; set; }

            public ZonedDateTime? NullableZonedDateTime { get; set; }
        }
    }
}