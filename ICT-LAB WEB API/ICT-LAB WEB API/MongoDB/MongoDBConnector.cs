using System.Configuration;
using MongoDB.Driver;
using ICT_LAB_WEB_API.Models;
using System.Collections.Generic;

namespace ICT_LAB_WEB_API.MongoDB
{
    public class MongoDBConnector
    {
        public IMongoDatabase database { get; set; }
        public IMongoClient client { get; set; }

        public MongoDBConnector()
        {
            string url = ConfigurationSettings.AppSettings["MongoDBURL"];
            try
            {
                client = new MongoClient(url);
                database = client.GetDatabase("local");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Could not connect to Database. " + ex);
                //TODO log
            }
            
        }


    }
}