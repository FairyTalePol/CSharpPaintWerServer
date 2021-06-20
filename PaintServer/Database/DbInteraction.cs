using System;
using System.Linq;

namespace PaintServer.Database
{
    public class DbInteraction
    {
        private AppContext _db;
        private User _user;
        private UserStatistics _userStatistics;

        public DbInteraction()
        {
            _db = AppContext.Create();
        }

        public void CreateUser(User user, string registrationDate, string lastActivity)
        {
            _user = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserPassword = user.UserPassword
            };

            _userStatistics = new UserStatistics
            {
                RegistrationDate = registrationDate,
                LastActivity = lastActivity
            };

            _db.Users.Add(_user);
            _db.Statistics.Add(_userStatistics);

            try
            {
                _db.SaveChanges();
            }
            catch
            {
                new Exception("Oops. Smth went wrong.");
            }
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


    }
}
