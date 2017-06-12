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
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Sensor")]
    [Authorize]
    public class SensorController : ApiController
    {
        private readonly ISensorService _sensorService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensorService"></param>
        public SensorController(ISensorService sensorService)
        {
            //_sensorService = new SensorService();
            _sensorService = sensorService;
        }

        /// <summary>
        /// Returns all sensors without the one in parameter
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns>IHttpActionResult with List of Sensors</returns>
        [Route("GetSensorsWithoutCurrent")]
        [HttpPost]
        public IHttpActionResult GetSensorsWithoutCurrent([FromBody] Sensor sensor)
        {
            var result = _sensorService.GetSensorsWithoutCurrent(sensor);

            return Ok(result);
        }

        /// <summary>
        /// returns list of sensors without their readings
        /// </summary>
        /// <param name="home"></param>
        /// <returns></returns>
        [Route("GetSensorsWithoutReadings")]
        [HttpGet]
        public IHttpActionResult GetSensorsWithoutReadings(string home)
        {
            var result = _sensorService.GetSensorsWithoutReadingsByHome(home);
            return Ok(result);
        }

        /// <summary>
        /// Creates a sensor
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns>IHttpActionResult</returns>
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

        /// <summary>
        /// Update an existing sensor
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns>IHttpActionResult</returns>
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

        /// <summary>
        /// Delete an existing sensor
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns>IHttpActionResult</returns>
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

        /// <summary>
        /// Get a sensor by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="home"></param>
        /// <returns>IHttpActionResult</returns>
        [Route("GetSensorBySensorId")]
        [HttpGet]
        public IHttpActionResult GetSensorBySensorId([FromUri] string id, string home)
        {
            var s = _sensorService.GetSensorBySensorId(id, home);
            
            return Ok(s);
        }
    }
}
