using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.MongoDatabase
{
    public class UserStatisticsNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        public int AmountBMP { get; set; }
        public int AmountJson { get; set; }
        public int AmountJPG { get; set; }
        public int AmountPNG { get; set; }
        public int AmountTotal { get; set; }
        public string RegistrationDate { get; set; }
        public string LastActivity { get; set; }

    }
}
