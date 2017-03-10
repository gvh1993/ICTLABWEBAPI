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
        readonly log4net.ILog logger;

        public MongoDBConnector()
        {
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            string url = ConfigurationSettings.AppSettings["MongoDBURL"];
            try
            {
                client = new MongoClient(url);
                database = client.GetDatabase("local");
            }
            catch (System.Exception ex)
            {
                logger.Error("Could not connect to database! " + ex);
            }
            
        }


    }
}