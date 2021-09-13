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
        [BsonIgnore]
        public List<PersonModel> Owners { get; set; }
        public int YearMade { get; set; }
    }
}
