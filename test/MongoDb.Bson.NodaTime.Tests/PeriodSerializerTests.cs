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
            BsonSerializer.RegisterSerializer(new PeriodSerializer());
        }

        [Fact]
        public void CanConvertValue()
        {
            var obj = new Test { Period = Period.FromSeconds(34) };
            obj.ToTestJson().Should().Contain("'Period' : 'PT34S'");

            obj = BsonSerializer.Deserialize<Test>(obj.ToBson());
            obj.Period.Should().Be(obj.Period);
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