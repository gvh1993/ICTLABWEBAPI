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
            //SensorViewModel sensors = new SensorViewModel();
            //try
            //{
            //    MongoDBConnector con = new MongoDBConnector();
            //    var result = con.database.ListCollections();

            //var collections = result.ToList();
            //foreach (var collection in collections)
            //{
            //    Sensor sensor = new Sensor();
            //    sensor.Name = collection["name"].AsString;

            //    if (sensor.Name != "system.version" && sensor.Name != "startup_log")
            //    {
            //        sensors.Sensors.Add(sensor);
            //    }
            //}
            //}
            //    catch (Exception ex)
            //    {
            //        logger.Error("Could not retrieve list collections from database. " + ex);
            //    }
            //return View(sensors);
            SensorViewModel model = new SensorViewModel();
            return View(model);
        }

        //sensor
        public ActionResult Add()
        {
            return View();
        }
    }
}