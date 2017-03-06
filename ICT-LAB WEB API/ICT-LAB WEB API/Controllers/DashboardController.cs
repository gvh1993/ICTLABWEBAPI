using System;
using System.Web.Http;
using System.Web.Mvc;
using ICT_LAB_WEB_API.Models;
using ICT_LAB_WEB_API.MongoDB;
using MongoDB.Driver;

namespace ICT_LAB_WEB_API.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Sensor
        public ActionResult Index()
        {
            MongoDBConnector con = new MongoDBConnector();

            var result = con.database.ListCollections();

            SensorViewModel sensors = new SensorViewModel();

            var collections = result.ToList();
            foreach (var collection in collections)
            {
                Sensor sensor = new Sensor();
                sensor.Name = collection["name"].AsString;

                sensors.Sensors.Add(sensor);
            }


            return View(sensors);
        }

        //sensor
        public ActionResult Add(string name)
        {
            return View();
        }
    }
}