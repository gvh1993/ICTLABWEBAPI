using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using ICTLAB.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using ICTLAB.Models;

namespace ICTLAB.Repositories
{
    public class SensorRepository : MongoDBConnector, ISensorRepository
    {
        public IAsyncCursor<BsonDocument> Get()
        {
            return database.ListCollections();
        }

        public bool Create(string sensorName)
        {
            try
            {
                database.CreateCollection(sensorName);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string collectionName)
        {
            try
            {
                database.DropCollection(collectionName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IMongoCollection<BsonDocument> GetCollectionByName(string sensorName)
        {
            return database.GetCollection<BsonDocument>(sensorName);
        }

        public bool InsertDocumentToSensor(BsonDocument document, string sensorName)
        {
            try
            {
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(sensorName);
                collection.InsertOne(document);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}