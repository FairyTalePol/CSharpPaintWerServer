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
