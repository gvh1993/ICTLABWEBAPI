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

namespace ICT_LAB_WEB_API.apiController
{
    public class SensorController : ApiController
    {
        
        [HttpPost]
        public IHttpActionResult Add([FromBody]string sensorName)
        {
            bool foundUnique = false;
            int count = 0;
            
            MongoDBConnector con = new MongoDBConnector();

            while (!foundUnique)
            {
                try
                {
                    con.database.CreateCollection(sensorName+count);
                    foundUnique = true;
                }
                catch (Exception)
                {
                    count++;
                }
            }

            return Ok(sensorName+count);
        }

        [HttpPost]
        public void Delete(string sensorName)
        {
            MongoDBConnector con = new MongoDBConnector();
            con.database.DropCollection(sensorName);

            
        }
    }
}
