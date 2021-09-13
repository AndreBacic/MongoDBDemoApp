using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBDemo
{
    public class MongoCRUD
    {
        private IMongoDatabase _db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            _db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id); // Eq id for equals, ctrl+J to see other comparisons

            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = _db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", new BsonBinaryData(id, GuidRepresentation.Standard)),
                record,
                new ReplaceOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id); // Eq id for equals, ctrl+J to see other comparisons
            collection.DeleteOne(filter);
        }

        public T AggregateRecord<T>(string localTable, string foreignTable, Guid id, 
            string localField, string foreignField, string asField)
        {
            var collection = _db.GetCollection<T>(localTable);
            var filter = Builders<T>.Filter.Eq("Id", id);

            var m = collection.Aggregate()
                .Match(filter)
                .Lookup(foreignTable, localField, foreignField, asField);
            var a = m.As<T>().ToList();
            return a.First();
        }
    }
}
