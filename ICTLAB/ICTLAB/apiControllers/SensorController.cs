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
    [Authorize]
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
