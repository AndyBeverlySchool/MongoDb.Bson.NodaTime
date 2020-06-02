using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using Xunit;
using FluentAssertions;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class OffsetDateTimeSerializerTests
    {
        static OffsetDateTimeSerializerTests()
        {
            BsonSerializer.RegisterSerializer(new OffsetDateTimeSerializer());
        }

        [Fact]
        public void CanRoundTripValueWithIsoCalendar()
        {
            var obj = new Test { OffsetDateTime = new LocalDateTime(2015, 1, 2, 3, 4, 5).WithOffset(Offset.FromHours(1)) };
            obj.ToTestJson().Should().Contain("'OffsetDateTime' : '2015-01-02T03:04:05+01'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.OffsetDateTime.Should().Be(obj.OffsetDateTime);
        }


        [Fact]
        public void ConvertsToIsoCalendarWhenSerializing()
        {
            var obj = new Test { OffsetDateTime = new LocalDateTime(2015, 1, 2, 3, 4, 5).WithOffset(Offset.FromHours(1)).WithCalendar(CalendarSystem.PersianSimple) };
            obj.ToTestJson().Should().Contain("'OffsetDateTime' : '2015-01-02T03:04:05+01'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.OffsetDateTime.Should().Be(obj.OffsetDateTime.WithCalendar(CalendarSystem.Iso));
        }

        [Fact]
        public void ThrowsWhenDateIsInvalid()
        {
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("OffsetDateTime", "bleh"))));
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("OffsetDateTime", BsonNull.Value))));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("NullableOffsetDateTime", BsonNull.Value))).NullableOffsetDateTime.Should().BeNull();
        }

        private class Test
        {
            public OffsetDateTime OffsetDateTime { get; set; }

            public OffsetDateTime? NullableOffsetDateTime { get; set; }
        }
    }
}