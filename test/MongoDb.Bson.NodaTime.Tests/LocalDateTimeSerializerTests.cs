using System;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using Xunit;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class LocalDateTimeSerializerTests
    {
        static LocalDateTimeSerializerTests()
        {
            BsonSerializer.RegisterSerializer(new LocalDateTimeSerializer());
        }

        [Fact]
        public void CanRoundTripValueWithIsoCalendar()
        {
            var obj = new Test { LocalDateTime = new LocalDateTime(2015, 1, 2, 3, 4, 5) };
            obj.ToTestJson().Should().Contain("'LocalDateTime' : '2015-01-02T03:04:05'");

            obj = BsonSerializer.Deserialize<Test>(obj.ToBson());
            obj.LocalDateTime.Should().Be(obj.LocalDateTime);
        }


        [Fact]
        public void ConvertsToIsoCalendarWhenSerializing()
        {
            var obj = new Test { LocalDateTime = new LocalDateTime(2015, 1, 2, 3, 4, 5).WithCalendar(CalendarSystem.PersianSimple) };
            obj.ToTestJson().Should().Contain("'LocalDateTime' : '2015-01-02T03:04:05'");

            obj = BsonSerializer.Deserialize<Test>(obj.ToBson());
            obj.LocalDateTime.Should().Be(obj.LocalDateTime.WithCalendar(CalendarSystem.Iso));
        }

        [Fact]
        public void ThrowsWhenDateIsInvalid()
        {
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("LocalDateTime", "bleh"))));
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("LocalDateTime", BsonNull.Value))));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("NullableLocalDateTime", BsonNull.Value))).NullableLocalDateTime.Should().BeNull();
        }

        private class Test
        {
            public LocalDateTime LocalDateTime { get; set; }

            public LocalDateTime? NullableLocalDateTime { get; set; }
        }
    }
}