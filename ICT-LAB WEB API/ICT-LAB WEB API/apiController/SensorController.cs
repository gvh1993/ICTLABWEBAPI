using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ICT_LAB_WEB_API.Models;
using ICT_LAB_WEB_API.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;


namespace ICT_LAB_WEB_API.apiController
{
    public class SensorController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            MongoDBConnector con = new MongoDBConnector();

            var result = con.database.ListCollections();

            SensorViewModel sensors = new SensorViewModel();

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

            return Ok(sensors);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]Sensor sensor)
        {
            bool foundUnique = false;
            int count = 0;
            
            MongoDBConnector con = new MongoDBConnector();

            while (!foundUnique)
            {
                try
                {
                    sensor.Name = sensor.Type + count;

                    con.database.CreateCollection(sensor.Name);

                    BsonDocument document = new BsonDocument().AddRange(sensor.ToBsonDocument());
                    
                    con.database.GetCollection<BsonDocument>(sensor.Name).InsertOne(document);
                    
                    foundUnique = true;
                }
                catch (Exception)
                {
                    count++;
                }
            }

            return Ok(sensor.Type+count);
        }

        [HttpPost]
        public void Delete(string sensorName)
        {
            MongoDBConnector con = new MongoDBConnector();
            con.database.DropCollection(sensorName);
        }
    }
}
