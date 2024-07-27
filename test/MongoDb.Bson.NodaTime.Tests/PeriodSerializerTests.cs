using System;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using Xunit;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class PeriodSerializerTests
    {
        static PeriodSerializerTests()
        {
            var options = new NodaTimeSerializerOptions();
            BsonSerializer.RegisterSerializer(new PeriodSerializer(options.PeriodPattern));
        }

        [Fact]
        public void CanConvertValue()
        {
            var obj = new Test { Period = Period.FromSeconds(34) };
            obj.ToTestJson().Should().Contain("'Period' : 'PT34S'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.Period.Should().Be(obj.Period);
        }


        [Fact]
        public void ThrowsWhenValueIsInvalid()
        {
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("Period", "bleh"))));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("Period", BsonNull.Value))).Period.Should().BeNull();
        }

        private class Test
        {
            public Period Period { get; set; }
        }
    }
}