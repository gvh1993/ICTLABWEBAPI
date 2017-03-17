using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using ICTLAB.Models;
using ICTLAB.Services;


namespace ICTLAB.ApiControllers
{
    public class SensorController : ApiController
    {
        private ISensorService sensorService;

        public SensorController()
        {
            sensorService = new SensorService();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            //give list of all sensors
            var result = sensorService.Get();
            

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]Sensor sensor)
        {
            //bool foundUnique = false;
            //int count = 0;

            //while (!foundUnique)
            //{
            //    try
            //    {
            //        sensor.Name = sensor.Type + count;

            //        con.database.CreateCollection(sensor.Name);

            //        BsonDocument document = new BsonDocument().AddRange(sensor.ToBsonDocument());

            //        con.database.GetCollection<BsonDocument>(sensor.Name).InsertOne(document);

            //        foundUnique = true;
            //    }
            //    catch (Exception)
            //    {
            //        count++;
            //    }
            //}
            var result = sensorService.CreateFirstAvailableSensorName(sensor);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Delete(string sensorName)
        {
            if (sensorService.DeleteSensorByName(sensorName))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
            
        }
    }
}
