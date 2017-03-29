using ICTLAB.Models;
using ICTLAB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ICTLAB.apiControllers
{
    
    public class HomeController : ApiController
    {
        IHomeService homeService;
        public HomeController()
        {
            homeService = new HomeService();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            List<Home> homes = homeService.GetAllHomes();

            return Ok(homes);
        }

        [HttpPut]
        public IHttpActionResult Create([FromBody]Home home)
        {
            bool result = homeService.Create(home.Name);
            if (result)
            {
                //get the name
                return Ok(home.Name);
            }
            else
            {
                return InternalServerError();
            }
        }
        
        [HttpDelete]
        public IHttpActionResult Delete(string name)
        {
            bool result = homeService.Delete(name);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return InternalServerError();
            }
            
        }
    }
}
