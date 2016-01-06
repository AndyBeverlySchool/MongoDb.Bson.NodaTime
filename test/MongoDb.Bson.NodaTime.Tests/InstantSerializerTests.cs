using System;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using NodaTime;
using Xunit;

namespace MongoDb.Bson.NodaTime.Tests
{
    public class InstantSerializerTests
    {
        static InstantSerializerTests()
        {
            BsonSerializer.RegisterSerializer(new InstantSerializer());
        }

        [Fact]
        public void CanConvertValid()
        {
            var instant = Instant.FromUtc(2015, 1, 1, 0, 0, 1);
            var obj = new Test { Instant = instant };
            obj.ToTestJson().Should().Contain("'Instant' : NumberLong('14200704010000000')");

            obj = BsonSerializer.Deserialize<Test>(obj.ToBson());
            obj.Instant.Should().Be(instant);
        }

        [Fact]
        public void CanConvertNullableValid()
        {
            var instant = Instant.FromUtc(2015, 1, 1, 0, 0, 1);
            var obj = new Test { InstantNullable = instant };
            obj.ToTestJson().Should().Contain("'InstantNullable' : NumberLong('14200704010000000')");

            obj = BsonSerializer.Deserialize<Test>(obj.ToBson());
            obj.InstantNullable.Should().Be(instant);
        }

        [Fact]
        public void CanConvertNullableNull()
        {
            var instant = Instant.FromUtc(2015, 1, 1, 0, 0, 1);
            var obj = new Test { Instant = instant };
            obj.ToTestJson().Should().Contain("'Instant' : NumberLong('14200704010000000')");
            obj.ToTestJson().Should().Contain("'InstantNullable' : null");

            obj = BsonSerializer.Deserialize<Test>(obj.ToBson());
            obj.Instant.Should().Be(instant);
            obj.InstantNullable.Should().BeNull();
        }

        [Fact]
        public void CanConvertEitherStringOrInt64()
        {
            var instant = Instant.FromUtc(2015, 1, 1, 0, 0, 1);

            var doc = new BsonDocument(new BsonElement("Instant", new BsonString("14200704010000000")));
            var obj = BsonSerializer.Deserialize<Test>(doc);
            obj.Instant.Should().Be(instant);

            doc = new BsonDocument(new BsonElement("Instant", new BsonInt64(14200704010000000)));
            obj = BsonSerializer.Deserialize<Test>(doc);
            obj.Instant.Should().Be(instant);
        }

        [Fact]
        public void ThrowsForInvalidTypes()
        {
            var doc = new BsonDocument(new BsonElement("Instant", new BsonBoolean(false)));
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(doc));

            doc = new BsonDocument(new BsonElement("Instant", new BsonInt32(1)));
            Assert.Throws<FormatException>(() => BsonSerializer.Deserialize<Test>(doc));
        }

        [Fact]
        public void CanParseNullable()
        {
            BsonSerializer.Deserialize<Test>(new BsonDocument(new BsonElement("InstantNullable", BsonNull.Value))).InstantNullable.Should().BeNull();
        }

        private class Test
        {
            public Instant Instant { get; set; }

            public Instant? InstantNullable { get; set; }
        }
    }
}