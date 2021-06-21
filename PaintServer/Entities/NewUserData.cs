using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaintServer.Entities
{
    public class NewUserData
    {
        private string _regexNameLastName = "^[A-Za-z](-?[A-Za-z]{1,14})?-?([A-Za-z]{1,15})?$";
        private string _regexEmail = "^[0-9A-Za-z]+(.?[0-9A-Za-z]+){2,29}.?[0-9A-Za-z]+@[a-z]+.[a-z]{2,4}$";

        public string Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string Email { get; set; }

        public string UserPassword { get; set; }

        public void Validate()
        {
            bool result = true;
            Regex regex = new Regex(_regexEmail);
            if (Email != null)
            {
                if (!regex.IsMatch(Email))
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }


            regex = new Regex(_regexNameLastName);
            if (FirstName != null)
            {
                if (!regex.IsMatch(FirstName))
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            if (LastName != null)
            {
                if (!regex.IsMatch(LastName))
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            if (UserPassword!= null)
            {
                if (UserPassword.Length<6||UserPassword.Length>30)
                {
                    result = false;

                }
            }
            else
            {
                result = false;
            }

            if (result==false)
            {
                throw new ArgumentException("User Data validation failed");
            }
        }
    }
}
