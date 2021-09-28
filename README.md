[![Build status](https://ci.appveyor.com/api/projects/status/85k2xjjhxms1l464?svg=true)](https://ci.appveyor.com/project/tetious/mongodb-bson-nodatime)

# MongoDb.Bson.NodaTime
BsonSerializers for NodaTime types, for use with the MongoDB C# driver.

# Supported NodaTime versions

The major version of MongoDb.Bson.NodaTime is in sync with the major version of NodaTime.

# Installation
Grab the latest package from Nuget. Yay, Nuget!

# Usage
To register all the serializers in the package call the static registration method:

```
NodaTimeSerializers.Register();
```

**IMPORTANT:** You must register any/all serializers you wish to use 
before you use any of the Mongo driver's serialization features, including registering class mappers.

If you'd like to use specific serializers only, you can register them 
individually like this:

```
BsonSerializer.RegisterSerializer(new InstantSerializer());

```
# Special case for Instant, as of v3.0.0

By default, Instants are  serialized to be compatible with Mongo's underling Date representation, which is limited to ms 
precision and is represented in the BSON as "a 64-bit integer that represents the number of milliseconds since the Unix epoch (Jan 1, 1970).

You can use NodaTime's extended iso pattern instead, which has tick precision, by setting the static property `InstantSerializer.UseExtendedIsoStringPattern`. If you do this, you will need to use caution and test your queries, as the underlying field will no longer be a Mongo Date, but a plain string.

This will likely break certain types of aggregations, as well as greater than/less than, etc.

# Implemented NodaTime Types (as of v2.1.0)

* Instant
* LocalDate
* LocalTime
* LocalDateTime
* OffsetDateTime
* Period
* Duration
* ZonedDateTime
* Offset
