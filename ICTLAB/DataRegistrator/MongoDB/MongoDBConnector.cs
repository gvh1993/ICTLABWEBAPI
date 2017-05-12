using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRegistrator.MongoDB
{
    public class MongoDBConnector
    {
        public IMongoDatabase database { get; set; }
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
            }
            catch (System.Exception ex)
            {
                logger.Error("Could not connect to database! " + ex);
            }
        }
    }
}

