using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICTLAB.apiControllers
{
    [RoutePrefix("api/Model")]
    public class ModelController : ApiController
    {
        [Route("chibbHouse")]
        [HttpGet]
        public IHttpActionResult GetChibbHouse()
        {
            var json = "";
            //get json
            try
            {
                //var json = File.Open("~/Content/Models/chibb.json", FileMode.Open);
                json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Models/chibb.json"));
            }
            catch (Exception ex)
            {
                
            }
            

            //return json
            return Ok(json);
        }
    }
}
