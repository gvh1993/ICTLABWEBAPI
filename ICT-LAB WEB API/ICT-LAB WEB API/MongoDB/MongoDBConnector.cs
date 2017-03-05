using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace ICT_LAB_WEB_API.MongoDB
{
    public class MongoDBConnector
    {
        public IMongoDatabase database { get; set; }
        public IMongoClient client { get; set; }

        public void Connect()
        {
            string url = ConfigurationSettings.AppSettings["MongoDBURL"];

            client = new MongoClient(url);
            database = client.GetDatabase("admin");
        }


    }
}