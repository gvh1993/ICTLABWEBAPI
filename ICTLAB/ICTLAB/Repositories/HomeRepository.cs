using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICTLAB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using log4net;

namespace ICTLAB.Repositories
{
    public class HomeRepository : MongoDB.MongoDBConnector, IHomeRepository
    {
        ILog logger;

        public HomeRepository()
        {
            logger = log4net.LogManager.GetLogger(typeof(HomeRepository));
        }

        //get home
        public IMongoCollection<BsonDocument> GetByName(string name)
        {
            try
            {
                var x = database.GetCollection<BsonDocument>(name);
                return x;
            }
            catch (Exception ex)
            {
                logger.Error("Unable to get collection by the name of " + name, ex);
                return null;
            }
        }

        //get all homes
        public IAsyncCursor<BsonDocument> Get()
        {
            try
            {
                return database.ListCollections();
            }
            catch (Exception ex)
            {
                logger.Error("Could not retrieve homes from DB", ex);
                return null;
            }
        }

        //create home
        public bool Create(string name)
        {
            try
            {
                database.CreateCollection(name);
            }
            catch (Exception ex)
            {
                logger.Error("Could not create: "+name, ex);
                return false;
            }

            return true;
        }

        //delete home
        public bool Delete(string name)
        {
            try
            {
                database.DropCollection(name);
            }
            catch (Exception ex)
            {
                logger.Error("Could not delete collection: "+name, ex);
                return false;
            }

            return true;
        }


    }
}