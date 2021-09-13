using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoDBDemo
{
    [BsonIgnoreExtraElements]
    public class CarModel
    {
        [BsonId] // mongo _id
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public PersonModel Owner { get; set; }
        public bool ShouldSerializeOwner() { return false; }
        public int YearMade { get; set; }
    }
}
