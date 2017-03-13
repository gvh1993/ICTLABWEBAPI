using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICT_LAB_WEB_API.Helper;
using System.ComponentModel.DataAnnotations;

namespace ICT_LAB_WEB_API.Models
{
    public class User
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Enums.UserRoles Role { get; set; }
    }
}