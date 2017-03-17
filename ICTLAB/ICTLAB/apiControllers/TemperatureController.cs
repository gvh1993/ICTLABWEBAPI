using System.Diagnostics;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ICTLAB.ApiControllers
{
    public class TemperatureController : ApiController
    {
        [Authorize]
        //GET api/temperatures
        public BsonDocument Get()
        {
            //TODO
            return new BsonDocument();
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
