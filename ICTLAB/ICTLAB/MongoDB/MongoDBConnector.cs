using System.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ICTLAB.MongoDB
{
    public class MongoDBConnector
    {
        public IMongoDatabase database { get; set; }
        public IMongoDatabase userDatabase { get; set; }
        public IMongoClient client { get; set; }
        readonly log4net.ILog logger;

        public MongoDBConnector()
        {
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            string url = ConfigurationManager.AppSettings["MongoDBURL"];

            try
            {
                client = new MongoClient(url);
                database = client.GetDatabase("local");
                userDatabase = client.GetDatabase("admin");
            }
            catch (System.Exception ex)
            {
                logger.Error("Could not connect to database! " + ex);
            }
        }
    }
}