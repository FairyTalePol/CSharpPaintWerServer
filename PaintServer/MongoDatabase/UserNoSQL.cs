﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.MongoDatabase
{
    public class UserNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public UserStatisticsNoSQL Statistics { get; set; }
       // public ICollection<PictureNoSQL> UserPictures { get; set; }
    }
}