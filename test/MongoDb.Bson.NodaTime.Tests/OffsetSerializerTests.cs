using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using System;
using Xunit;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class OffsetSerializerTests
    {
        static OffsetSerializerTests()
        {
            var options = new NodaTimeSerializerOptions();
            BsonSerializer.RegisterSerializer(new OffsetSerializer(options.OffsetPattern));
        }

        [Fact]
        public void CanConvertValue()
        {
            var obj = new Test { Offset = Offset.FromHours(4) };
            obj.ToTestJson().Should().Contain("'Offset' : '+04'");

            var deserialized = BsonSerializer.Deserialize<Test>(obj.ToBson());
            deserialized.Offset.Should().Be(obj.Offset);
        }

        [Fact]
        public void ThrowsWhenValueIsInvalid()
        {
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("Duration", "bleh"))));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("OffsetNullable", BsonNull.Value))).OffsetNullable.Should().BeNull();
        }

        private class Test
        {
            public Offset Offset { get; set; }
            public Offset? OffsetNullable { get; set; }
        }
    }
}
