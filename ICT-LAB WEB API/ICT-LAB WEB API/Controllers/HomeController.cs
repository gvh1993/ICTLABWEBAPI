using ICT_LAB_WEB_API.Helper;
using ICT_LAB_WEB_API.Models;
using log4net;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace ICT_LAB_WEB_API.Controllers
{
    
    public class HomeController : Controller
    {
        readonly ILog logger;
        public HomeController()
        {
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            User user = new User() { Role = Enums.UserRoles.Visitor };
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            //check passwords
            /* Fetch the stored value */
            MongoDB.MongoDBConnector con = new MongoDB.MongoDBConnector();
            var collection = con.userDatabase.GetCollection<BsonDocument>(user.Role.ToString() + "s");
            var filter = Builders<BsonDocument>.Filter.Eq("Email", user.Email);
            var foundUsers = collection.FindSync<UserLogin>(filter).ToList();

            if (!foundUsers.Any())
            {
                // if user not found
                TempData["Error"] = "Gebruiker is niet gevonden.";
                return View();
            }

            string savedPasswordHash = foundUsers.FirstOrDefault().Password;

            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    TempData["Error"] = "Password is wrong.";
                    return View();
                }
            }

            // create session
            Session["User_Role"] = user.Role;
            Session["User_Email"] = user.Email;
            return RedirectToAction("Index", "Home");
        }

        [AuthorizeUser(UserRole = "Admin, Visitor")]
        public ActionResult Logout()
        {
            Session["User_Role"] = null;
            Session["User_Email"] = null;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            User user = new User() { Role = Enums.UserRoles.Visitor };
            return View(user);
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            User register = new User
            {
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            MongoDB.MongoDBConnector con = new MongoDB.MongoDBConnector();

            try
            {
                // check if alredy exist
                var filter = Builders<BsonDocument>.Filter.Eq("Email", user.Email);
                var foundUsers = con.userDatabase.GetCollection<BsonDocument>(user.Role + "s").Find<BsonDocument>(
                    filter
                    );

                if (foundUsers.Any())
                {
                    TempData["Error"] = "User already exists";
                    return View();
                }
            }
            catch (Exception ex)
            {

            }
            //hash + salt password
            register.Password = CodePassword(user.Password);
            var document = new BsonDocument().AddRange(register.ToBsonDocument());

            //Register
            try
            {
                
                con.userDatabase.GetCollection<BsonDocument>(user.Role.ToString() + "s").InsertOne(document);
            }
            catch (Exception ex)
            {
                logger.Error("Could not connect to Database. " + ex);
            }

            return RedirectToAction("Login", "Home");
        }

        private string CodePassword(string password)
        {
            //1 Create the salt value with a cryptographic PRNG:
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //2 Create the Rfc2898DeriveBytes and get the hash value:
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //3 Combine the salt and password bytes for later use:
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //4 Turn the combined salt+hash into a string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }



    }


}
