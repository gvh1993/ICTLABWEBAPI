using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICTLAB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ICTLAB.Repositories
{
    public class HomeRepository : MongoDB.MongoDBConnector, IHomeRepository
    {

        public HomeRepository()
        {

        }

        //get home
        public IMongoCollection<BsonDocument> GetByName(string name)
        {
            try
            {
                return database.GetCollection<BsonDocument>(name);
            }
            catch (Exception ex)
            {
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
                return false;
            }

            return true;
        }


    }
}