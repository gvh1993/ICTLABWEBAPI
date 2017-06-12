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
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class HomeController : ApiController
    {
        readonly IHomeService _homeService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="homeService"></param>
        public HomeController(IHomeService homeService)
        {
            //_homeService = new HomeService();
            _homeService = homeService;
        }

        /// <summary>
        /// Get a list of all homes
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<Home> homes = _homeService.GetAllHomes();

            return Ok(homes);
        }

        /// <summary>
        /// Create a home
        /// </summary>
        /// <param name="home"></param>
        /// <returns>IHttpActionResult</returns>
        [HttpPut]
        public IHttpActionResult Create([FromBody]Home home)
        {
            bool result = _homeService.Create(home.Name);
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

        /// <summary>
        /// Delete a home with al of it's sensors
        /// </summary>
        /// <param name="name"></param>
        /// <returns>IHttpActionResult</returns>
        [HttpDelete]
        public IHttpActionResult Delete(string name)
        {
            bool result = _homeService.Delete(name);
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
