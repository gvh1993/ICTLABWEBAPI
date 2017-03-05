using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ICT_LAB_WEB_API.MongoDB;
using MongoDB.Bson;
using MongoDB;
using MongoDB.Driver;
using WebGrease.Css.Extensions;

namespace ICT_LAB_WEB_API.Controllers
{
    public class TemperatureController : ApiController
    {
        //GET api/temperatures
        public BsonDocument Get()
        {
            //get all form database
            MongoDBConnector connector = new MongoDBConnector();
            connector.Connect();

            var collection = connector.database.GetCollection<BsonDocument>("Temperature");

            var result = collection.Find(_ => true).ToList();
            foreach (var document in result)
            {
                foreach (var pair in document)
                {
                    Debug.WriteLine(pair.Name + " " + pair.Value);
                }
            }

            return result[0];
        }

        //GET api/temperature/{id}
        public float Get(float id)
        {
            // get from database with ID
            return id;
        }

        // POST api/values
        public void Post([FromBody]string temperature)
        {
            Debug.WriteLine(temperature);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string temperature)
        {
            Debug.WriteLine(temperature);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

    }
}
