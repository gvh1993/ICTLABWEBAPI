using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICTLAB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using log4net;
using MongoDB.Driver.Core.Operations;

namespace ICTLAB.Repositories
{
    public class HomeRepository : MongoDB.MongoDBConnector, IHomeRepository
    {
        readonly ILog _logger;

        public HomeRepository()
        {
            _logger = log4net.LogManager.GetLogger(typeof(HomeRepository));
        }

        //get all homes
        public IAsyncCursor<BsonDocument> Get()
        {
            // IF using this method: check return value for null!!
            try
            {
                return database.ListCollections();
            }
            catch (Exception ex)
            {
                _logger.Error("Could not retrieve homes from DB", ex);
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
                _logger.Error("Could not create: "+name, ex);
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
                _logger.Error("Could not delete collection: "+name, ex);
                return false;
            }

            return true;
        }


    }
}