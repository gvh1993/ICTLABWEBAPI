using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using ICT_LAB_WEB_API.Helper;

namespace ICT_LAB_WEB_API.Models
{
    public class UserLogin
    {
        public ObjectId _id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Enums.UserRoles Role { get; set; }
    }
}