using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using Xunit;
using FluentAssertions;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class LocalTimeSerializerTests
    {
        static LocalTimeSerializerTests()
        {
            BsonSerializer.RegisterSerializer(new LocalTimeSerializer());
        }

        [Fact]
        public void CanRoundTripValueWithIsoCalendar()
        {
            var obj = new Test { LocalTime = new LocalTime(13, 25, 1) };
            obj.ToTestJson().Should().Contain("'LocalTime' : '13:25:01'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.LocalTime.Should().Be(obj.LocalTime);
        }

        [Fact]
        public void ThrowsWhenDateIsInvalid()
        {
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("LocalTime", "bleh"))));
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("LocalTime", BsonNull.Value))));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("NullableLocalTime", BsonNull.Value))).NullableLocalTime.Should().BeNull();
        }

        private class Test
        {
            public LocalTime LocalTime { get; set; }

            public LocalTime? NullableLocalTime { get; set; }
        }
    }
}