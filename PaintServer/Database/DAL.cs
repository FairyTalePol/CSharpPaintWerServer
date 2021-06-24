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
                return "-1";
            }
            
            return userId;
        }

        public void SaveJson(int id, string lastActivity)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Id == id);
            if (statistics != null)
            {
                statistics.AmountJson++;
                statistics.LastActivity = lastActivity;
                _db.SaveChanges();
            }
        }

        public void SaveBMP(int id, string lastActivity)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Id == id);
            if (statistics != null)
            {
                statistics.AmountBMP++;
                statistics.LastActivity = lastActivity;
                _db.SaveChanges();
            }
        }

        public void SaveJPG(int id, string lastActivity)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Id == id);
            if (statistics != null)
            {
                statistics.AmountJPG++;
                statistics.LastActivity = lastActivity;
                _db.SaveChanges();
            }
        }

        public void SavePNG(int id, string lastActivity)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Id == id);
            if (statistics != null)
            {
                statistics.AmountPNG++;
                statistics.LastActivity = lastActivity;
                _db.SaveChanges();
            }
        }

        public UserStatistics GetUserStatistics(int id)
        {
            UserStatistics statistics = _db.Statistics.First(s => s.User.Id == id);
            return statistics;
        }

        public User GetUserById(int id)
        {
            //var users = _db.Users.Where(p => p.Id == id);
            try
            {
                User user = _db.Users.First(u => u.Id == id);
                return user;
            }
            //User res;
            //if (users.Count() > 0)
            //{
            //    res = users.ElementAt(0);
            //}
          //  else
          catch
            {
                throw new ArgumentNullException("User not found");
            }
           // return users.ElementAt(0);
        }

        public void UpdateUserStatistics(int id, string pictureType)
        {
            
            UserStatistics stat = _db.Statistics.First(p => p.Id == id);
          

            if (pictureType=="JSON")
            {
                stat.AmountJson += 1;

            }
            else if (pictureType=="BMP")
            {
                stat.AmountBMP += 1;
            }
            else if(pictureType=="JPG")
            {
                stat.AmountJPG++;
            }
            else if(pictureType=="PNG")
            {
                stat.AmountPNG++;
            }

            stat.LastActivity = DateTime.Now.ToString();
           
            _db.Statistics.Attach(stat);
            _db.Entry(stat).Property(x => x.AmountBMP).IsModified = true;
            _db.Entry(stat).Property(x => x.AmountJson).IsModified = true;
            _db.Entry(stat).Property(x => x.AmountJPG).IsModified = true;
            _db.Entry(stat).Property(x => x.AmountPNG).IsModified = true;
            _db.Entry(stat).Property(x => x.LastActivity).IsModified = true;
            _db.SaveChanges();
            
        }


        public Pictures[] GetPicturesByUserId(int id)
        {
            var pics = _db.Pictures.Where(p => p.UserId == id);
            Pictures[] res = new Pictures[pics.Count()];
            int i = 0;
            foreach(Pictures p in pics)
            {
                res[i] = p;
                i++;
            }
            return res;
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
