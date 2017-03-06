using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ICT_LAB_WEB_API.MongoDB;

namespace ICT_LAB_WEB_API.apiController
{
    public class SensorController : ApiController
    {
        public IHttpActionResult Add(string sensorName)
        {
            MongoDBConnector con = new MongoDBConnector();
            
        
            return Ok();
        }
    }
}
