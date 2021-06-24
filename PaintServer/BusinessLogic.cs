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

        public int AddPicture(PictureData picture)
        {
            int id = -1;

            //проверить существует ли пользователь с прилетевшим id
            User user;
            //try
            //{
                user = dal.GetUserById(picture.UserId);
            //}
            //catch
            //{
            //    throw new ArgumentException();
            //}


            if (user!=null)
            {
                try
                {
                    Pictures p = new Pictures();
                    p.UserId = picture.UserId;
                    p.Picture = picture.Picture;
                    p.PictureType = picture.Type;
                    id = dal.AddPicture(p);
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException();
                }
            }

            if (id!=-1)//если картинка реально добавилась, работаем со статистикой
            {
                dal.UpdateUserStatistics(picture.UserId, picture.Type);
            }
           
            return id;
        }

        public PictureData[] GetPictures(int userId)
        {
            Pictures[] pics = null;
            pics = dal.GetPicturesByUserId(userId);

            PictureData[] res = new PictureData[pics.Length];

            for (int i=0; i<pics.Length; i++)
            {
                res[i] = new PictureData();
                res[i].Picture = pics[i].Picture;
                res[i].Type = pics[i].PictureType;
                res[i].UserId = pics[i].UserId;
            }

            return res;
        }

        public string CheckUser(string email, string password)
        {
            string userId = dal.CheckUser(email, password, DateTime.Now.ToString());
            return userId;
        }

    }
}
