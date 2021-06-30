using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer.MongoDatabase
{
    public class DALNoSQL
    {
        private static DALNoSQL _dal;
        private MongoDatabase _db;

        private DALNoSQL()
        {
            _db = MongoDatabase.Create();
        }

        public static DALNoSQL Create()
        {
            if(_dal==null)
            {
                _dal = new DALNoSQL();
            }

            return _dal;
        }


        public void CreateUser(UserNoSQL user)
        {
            var collection = _db.GetCollection();
            collection.InsertOne(user);
        }

        public bool IsUserUnique(UserNoSQL user)
        {
            bool isUserUnique = true;
            var collection = _db.GetCollection();
            try
            {
                UserNoSQL user1 = GetUserByEmail(user.Email);
                isUserUnique = false;
            }
            catch
            {
                return isUserUnique;
            }

            return isUserUnique;
           
        }

        public UserNoSQL GetUserByEmail(string email)
        {
            var collection = _db.GetCollection();
            var filter = Builders<UserNoSQL>.Filter.Eq("Email", email);
            try
            {
                UserNoSQL user= collection.Find(filter).First();
                user.Statistics.LastActivity = DateTime.Now.ToString();
                return user;
            }
            catch
            {
                throw new ArgumentNullException("User not found");
            }
        }

        public void ChangePassword(string email, string password)
        {
            UserNoSQL user = GetUserByEmail(email);
            user.UserPassword = password;
            user.Statistics.LastActivity = DateTime.Now.ToString();
            UpdateUser(email, user);
        }

        public void UpdateUserStatistics(string email, string pictureType)
        {
            UserNoSQL user = GetUserByEmail(email);

            if (pictureType == "JSON")
            {
                user.Statistics.AmountJson++;
                user.Statistics.AmountTotal++;

            }
            else if (pictureType == "BMP")
            {
                user.Statistics.AmountBMP++;
                user.Statistics.AmountTotal++;
            }
            else if (pictureType == "JPEG")
            {
                user.Statistics.AmountJPG++;
                user.Statistics.AmountTotal++;
            }
            else if (pictureType == "PNG")
            {
                user.Statistics.AmountPNG++;
                user.Statistics.AmountTotal++;
            }

            user.Statistics.LastActivity = DateTime.Now.ToString();

            UpdateUser(email, user);
        }

        public List<UserNoSQL> GetAllUsers()
        {
            var collection = _db.GetCollection();
            return collection.Find(new BsonDocument()).ToList();
        }

        public void UpdateUser(string email, UserNoSQL user)
        {
            var collection = _db.GetCollection();
            collection.ReplaceOne(new BsonDocument("Email", email),user);
        }


    }
}
