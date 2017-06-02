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

        public ActionResult Details(string id, string sensorId)
        {
            ViewBag.ID = id;
            ViewBag.SensorId = sensorId;
            return View();
        }

        public ActionResult Manage(string id)
        {
            ViewBag.ID = id;
            return View();
        }
    }
}