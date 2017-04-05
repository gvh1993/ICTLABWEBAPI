using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ICTLAB.Controllers
{
    [System.Web.Mvc.Authorize]
    public class SensorController : Controller
    {
        // GET: Sensor
        public ActionResult Index([FromUri]string id)
        {
            ViewBag.ID = id;
            return View();
        }

        public ActionResult Add(string id)
        {
            ViewBag.ID = id;
            return View();
        }
    }
}