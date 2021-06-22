using PaintServer.Entities;
using System;
using System.Linq;

namespace PaintServer.Database
{
    public class DAL
    {
        private AppContext _db;

        private static DAL _dal;

        private DAL()
        {        
            _db = AppContext.Create();
        }

        public static DAL Create()
        {
            if (_dal == null)
            {
                _dal = new DAL();
            }
            return _dal;
        }

        public string CreateUser(User user, string registrationDate, string lastActivity)
        {
            _db.Users.Add(user);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                new Exception("Oops. Smth went wrong.");
            }

            UserStatistics userStatistics = new UserStatistics
            {
                RegistrationDate = registrationDate,
                LastActivity = lastActivity
              

            };
            userStatistics.UserId = user.Id;
           
            _db.Statistics.Add(userStatistics);

            try
            {
                _db.SaveChanges();
            }
            catch
            {
                new Exception("Oops. Smth went wrong.");
            }

            return user.Id.ToString();
        }


        public int AddPicture(Pictures picture)
        {
            _db.Pictures.Add(picture);
            //try
            //{
                _db.SaveChanges();
            //}
            //catch
            //{
            //    new Exception("Oops. Smth went wrong.");
            //}
            return picture.Id;
        }

        public string CheckUser(string email, string password)
        {
            string userId="";
            try
            {
                User user = _db.Users.First(u => u.Email == email);
                if(user.UserPassword==password)
                {
                    userId=user.Id.ToString();
                }
            }
            catch
            {
                throw new ArgumentNullException("No user found");
            }
            
            return userId;
        }

        public void SaveJson(string email, string lastActivity)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Email == email);
            if (statistics != null)
            {
                statistics.AmountJson++;
                statistics.LastActivity = lastActivity;
                _db.SaveChanges();
            }
        }

        public void SaveBMP(string email, string lastActivity)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Email == email);
            if (statistics != null)
            {
                statistics.AmountBMP++;
                statistics.LastActivity = lastActivity;
                _db.SaveChanges();
            }
        }

        public UserStatistics GetUserStatistics(string email)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Email == email);
            return statistics;
        }

        public User GetUserById(int id)
        {
            var users = _db.Users.Where(p => p.Id == id);
            User res;
            if (users.Count() > 0)
            {
                res = users.ElementAt(0);
            }
            else
            {
                throw new ArgumentException("User not found");
            }
            return users.ElementAt(0);
        }

        public void UpdateUserStatistics(int id, string pictureType)
        {
            var userStat = _db.Statistics.Where(p => p.Id == id);
            UserStatistics stat = null;
            if (userStat.Count() > 0)
            {
                stat = userStat.ElementAt(0);
            }
            else
            {
                throw new ArgumentException("User statistics does not exist");
            }

            if (pictureType=="JSON")
            {
                stat.AmountJson += 1;

            }
            else if (pictureType=="BMP")
            {
                stat.AmountBMP += 1;
            }
           
            _db.Statistics.Attach(stat);
            _db.Entry(stat).Property(x => x.AmountBMP).IsModified = true;
            _db.Entry(stat).Property(x => x.AmountJson).IsModified = true;
            _db.SaveChanges();
            
        }

        public void ShowAllUsers()
        {
            var Users = _db.Users;
            foreach (var u in Users)
            {
                Console.WriteLine($"{u.Email} {u.FirstName} {u.LastName} {u.UserPassword}");
            }

        }
    }
}
