[![Build status](https://ci.appveyor.com/api/projects/status/85k2xjjhxms1l464?svg=true)](https://ci.appveyor.com/project/tetious/mongodb-bson-nodatime)

# MongoDb.Bson.NodaTime
BsonSerializers for NodaTime types, for use with the MongoDB C# driver.

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

#Implemented NodaTime Types

(as of v1.0.0)
* Instant
* LocalDate
* LocalTime
* LocalDateTime
* OffsetDateTime
* Period

# Dependencies

* NodaTime v1.3.1
* MongoDB.Driver v2.2.0