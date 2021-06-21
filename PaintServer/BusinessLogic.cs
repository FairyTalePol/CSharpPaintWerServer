using PaintServer.Database;
using PaintServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintServer
{
    public class BusinessLogic
    {
        public DAL dal;
        static BusinessLogic _bl;
        

        private BusinessLogic()
        {
            dal = DAL.Create();
        }

        public static BusinessLogic Create()
        {
            if (_bl==null)
            {
                _bl = new BusinessLogic();
            }
            return _bl;
        }

        public NewUserData CreateUser(NewUserData user)
        {
          
            try
            {
                user.Validate();
                User u = new User();
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.UserPassword = user.UserPassword;
                u.Email = user.Email;
                
                user.Id = dal.CreateUser(u, DateTime.Now.ToString(), DateTime.Now.ToString());

                
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException();
            }
            return user;
        }

        public string CheckUser(string email, string password)
        {
            string userId = dal.CheckUser(email, password);
            return userId;
        }

    }
}
