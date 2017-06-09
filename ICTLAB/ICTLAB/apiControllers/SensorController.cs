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
    [RoutePrefix("api/Sensor")]
    [Authorize]
    public class SensorController : ApiController
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            //_sensorService = new SensorService();
            _sensorService = sensorService;
        }

        [Route("GetSensorsWithoutCurrent")]
        [HttpPost]
        public IHttpActionResult GetSensorsWithoutCurrent([FromBody] Sensor sensor)
        {
            var result = _sensorService.GetSensorsWithoutCurrent(sensor);

            return Ok(result);
        }

        [Route("GetSensorsWithoutReadings")]
        [HttpGet]
        public IHttpActionResult GetSensorsWithoutReadings(string home)
        {
            var result = _sensorService.GetSensorsWithoutReadingsByHome(home);
            return Ok(result);
        }

        [Route("AddSensor")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]CreateSensorViewModel sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _sensorService.Create(sensor);
                if (result)
                {
                    return Ok(sensor.Name);
                }
            return InternalServerError();
        }

        [Route("UpdateSensor")]
        [HttpPost]
        public IHttpActionResult Update([FromBody]Sensor sensor)
        {
            if (_sensorService.Update(sensor))
            {
                return Ok();
            }

            return InternalServerError();
        }


        [Route("DeleteSensor")]
        [HttpPost]
        public IHttpActionResult Delete([FromBody]Sensor sensor)
        {
            if (_sensorService.DeleteSensor(sensor))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [Route("GetSensorBySensorId")]
        [HttpGet]
        public IHttpActionResult GetSensorBySensorId([FromUri] string id, string home)
        {
            var s = _sensorService.GetSensorBySensorId(id, home);
            
            return Ok(s);
        }
    }
}
