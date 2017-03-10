using System;
using System.Web.Http;
using System.Web.Mvc;
using ICT_LAB_WEB_API.Models;
using ICT_LAB_WEB_API.MongoDB;
using MongoDB.Driver;
using log4net;

namespace ICT_LAB_WEB_API.Controllers
{
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
            SensorViewModel sensors = new SensorViewModel();
            try
            {
                MongoDBConnector con = new MongoDBConnector();
                var result = con.database.ListCollections();
            
            var collections = result.ToList();
            foreach (var collection in collections)
            {
                Sensor sensor = new Sensor();
                sensor.Name = collection["name"].AsString;

                if (sensor.Name != "system.version" && sensor.Name != "startup_log")
                {
                    sensors.Sensors.Add(sensor);
                }
            }
        }
            catch (Exception ex)
            {
                logger.Error("Could not retrieve list collections from database. " + ex);
            }
            return View(sensors);
        }

        //sensor
        public ActionResult Add()
        {
            return View();
        }
    }
}