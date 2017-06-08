using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICTLAB.Models;
using ICTLAB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ICTLAB.Services
{
    public class HomeService : IHomeService
    {
        readonly IHomeRepository _homeRepository;
        public HomeService()
        {
            _homeRepository = new HomeRepository();
        }

        public bool Create(string name)
        {
            return _homeRepository.Create(name);
        }

        public bool Delete(string name)
        {
            return _homeRepository.Delete(name);
        }

        public List<Home> GetAllHomes()
        {
            List<Home> homes = new List<Home>();

            var results = _homeRepository.Get();
            if (results == null)
                return new List<Home>();
            

            var collections = results.ToList();
            foreach (var collection in collections)
            {
                Home home = new Home()
                {
                    Name = collection["name"].AsString
                };
                if (home.Name != "system.version" && home.Name != "startup_log")
                {
                    homes.Add(home);
                }
            }
            return homes;
        }

    }
}