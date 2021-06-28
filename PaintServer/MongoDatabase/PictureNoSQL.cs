using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.MongoDatabase
{
    public class PictureNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        public string PictureType { get; set; }
        public string Picture { get; set; }
    }
}
