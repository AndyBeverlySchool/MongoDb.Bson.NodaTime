﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using NodaTime.Text;

namespace MongoDb.Bson.NodaTime
{
    public class PatternSerializerBase<TValue> : SerializerBase<TValue>
    {
        private readonly IPattern<TValue> pattern;
        private readonly Func<TValue, TValue> valueConverter;

        protected PatternSerializerBase(IPattern<TValue> pattern, Func<TValue, TValue> valueConverter = null)
        {
            this.pattern = pattern;
            this.valueConverter = valueConverter ?? (v => v);
        }

        public override TValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.GetCurrentBsonType();
            switch (type)
            {
                case BsonType.String:
                    return this.valueConverter(this.pattern.CheckedParse(context.Reader.ReadString()));
                default:
                    throw new NotSupportedException($"Cannot convert a {type} to a {typeof(TValue).Name}.");
            }
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValue value)
        {
            context.Writer.WriteString(this.pattern.Format(this.valueConverter(value)));
        }
    }
}