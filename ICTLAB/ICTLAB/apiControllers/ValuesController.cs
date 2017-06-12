using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ICTLAB.Models;

namespace ICTLAB.ApiControllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {

        private readonly Random _r;
        /// <summary>
        /// 
        /// </summary>
        public ValuesController()
        {
            _r = new Random();
        }

        /// <summary>
        /// Returns a random temperature value between 0 - 25
        /// </summary>
        /// <returns>Reading object {TimeStamp: DateTime, Value: string}</returns>
        [Route("Temperature")]
        [HttpGet]
        public Reading GetTemperature()
        {
            Reading reading = new Reading()
            {
                TimeStamp = DateTime.Now,
                Value = _r.Next(0, 25)
            };

            return reading;
        }

        /// <summary>
        /// Returns a random wind value from 0 kmph to 60 kmph
        /// </summary>
        /// <returns>Reading object {TimeStamp: DateTime, Value: string}</returns>
        [Route("Wind")]
        [HttpGet]
        public Reading GetWind()
        {
            Reading reading = new Reading()
            {
                TimeStamp = DateTime.Now,
                Value = _r.Next(0, 61)
            };

            return reading;
        }

        /// <summary>
        /// Returns a random moist value from 0 - 1024
        /// </summary>
        /// <returns>Reading object {TimeStamp: DateTime, Value: string}</returns>
        [Route("Moist")]
        [HttpGet]
        public Reading GetMoist()
        {
            Reading reading = new Reading()
            {
                TimeStamp = DateTime.Now,
                Value = _r.Next(0, 1024)
            };

            return reading;
        }

        /// <summary>
        /// Returns a random light/lux value from 0 to 100.000 lux
        /// </summary>
        /// <returns>Reading object {TimeStamp: DateTime, Value: string}</returns>
        [Route("Light")]
        [HttpGet]
        public Reading GetLight()
        {
            Reading reading = new Reading()
            {
                TimeStamp = DateTime.Now,
                Value = _r.Next(0, 100000)
            };

            return reading;
        }

        /// <summary>
        /// Returns a random humidity value from 0 percent to 100 percent
        /// </summary>
        /// <returns>Reading object {TimeStamp: DateTime, Value: string}</returns>
        [Route("Humidity")]
        [HttpGet]
        public Reading GetHumidity()
        {
            Reading reading = new Reading()
            {
                TimeStamp = DateTime.Now,
                Value = _r.Next(0, 100)
            };

            return reading;
        }
    }
}
