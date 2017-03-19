using System;
using System.Web.Mvc;
using ICTLAB.Models;
using log4net;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;

namespace ICTLAB.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        readonly ILog logger;
        public DashboardController()
        {
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        // GET: Sensor
        public ActionResult Index()
        {
            return View();
        }

        //sensor
        public ActionResult Add()
        {
            return View();
        }
    }
}