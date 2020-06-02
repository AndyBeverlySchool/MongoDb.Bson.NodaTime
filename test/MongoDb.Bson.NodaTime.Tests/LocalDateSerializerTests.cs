using System;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using Xunit;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class LocalDateSerializerTests
    {
        static LocalDateSerializerTests()
        {
            BsonSerializer.RegisterSerializer(new LocalDateSerializer());
        }

        [Fact]
        public void CanRoundTripValueWithIsoCalendar()
        {
            var obj = new Test { LocalDate = new LocalDate(2015, 1, 1) };
            obj.ToTestJson().Should().Contain("'LocalDate' : '2015-01-01'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.LocalDate.Should().Be(obj.LocalDate);
        }

        [Fact]
        public void ConvertsToIsoCalendarWhenSerializing()
        {
            var obj = new Test { LocalDate = new LocalDate(2015, 1, 1).WithCalendar(CalendarSystem.PersianSimple) };
            obj.ToTestJson().Should().Contain("'LocalDate' : '2015-01-01'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.LocalDate.Should().Be(obj.LocalDate.WithCalendar(CalendarSystem.Iso));
        }

        [Fact]
        public void ThrowsWhenDateIsInvalid()
        {
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("LocalDate", "bleh"))));
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("LocalDate", BsonNull.Value))));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("NullableLocalDate", BsonNull.Value))).NullableLocalDate.Should().BeNull();
        }

        private class Test
        {
            public LocalDate LocalDate { get; set; }

            public LocalDate? NullableLocalDate { get; set; }
        } 
    }
}