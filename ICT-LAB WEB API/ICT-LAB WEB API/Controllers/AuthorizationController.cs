using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICT_LAB_WEB_API.Controllers
{
    [AuthorizeUser(UserRole ="Admin, Visitor")]
    public abstract class AuthorizationController : Controller
    {

    }
}