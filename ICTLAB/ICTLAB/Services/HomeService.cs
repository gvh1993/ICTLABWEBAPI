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
        IHomeRepository homeRepository;
        public HomeService()
        {
            homeRepository = new HomeRepository();
        }

        public bool Create(string name)
        {
            return homeRepository.Create(name);
        }

        public bool Delete(string name)
        {
            return homeRepository.Delete(name);
        }

        public List<Home> GetAllHomes()
        {
            List<Home> homes = new List<Home>();

            var collections = homeRepository.Get().ToList();

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