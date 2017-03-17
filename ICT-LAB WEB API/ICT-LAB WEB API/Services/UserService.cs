using ICT_LAB_WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT_LAB_WEB_API.Services
{
    public class UserService
    {
        public User GetUserByCredentials(string email, string password)
        {
            User user = new User() { Email = "email@domain.com", Password = "password"};
            if (user != null)
            {
                user.Password = string.Empty;
            }
            return user;
        }
    }
}