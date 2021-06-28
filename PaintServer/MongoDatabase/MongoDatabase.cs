using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.MongoDatabase
{
    public class MongoDatabase
    {
        private static MongoDatabase _mongoDb;
        private MongoClient _client;
        private IMongoDatabase _db;
        private const string _dbName = "paintDb";
        private const string _collectionName = "Users";

        private MongoDatabase()
        {
            _client = new MongoClient();
            _db = _client.GetDatabase(_dbName);
        }

        public static MongoDatabase Create()
        {
            if(_mongoDb==null)
            {
                _mongoDb = new MongoDatabase();
            }

            return _mongoDb;
        }

        public IMongoCollection<UserNoSQL> GetCollection()
        {
            IMongoCollection<UserNoSQL> collection = _db.GetCollection<UserNoSQL>(_collectionName);
            return collection;
        }
    }
}
