using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ICTLAB.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        //// GET: Login
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Login
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public ActionResult LogOff()
        {
            //mvc signout
            var autheticationManager = HttpContext.GetOwinContext().Authentication;
            autheticationManager.SignOut();

            return View();
        }
    }
}