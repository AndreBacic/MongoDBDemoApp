using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDBDemo
{
    [BsonIgnoreExtraElements] // if you don't put this here, grabbing an item with extra fields crashes the program
    public class NameModel
    {
        [BsonId] // mongo _id
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
