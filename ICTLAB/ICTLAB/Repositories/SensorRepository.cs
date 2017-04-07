using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using ICTLAB.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using ICTLAB.Models;
using log4net;

namespace ICTLAB.Repositories
{
    public class SensorRepository : MongoDBConnector, ISensorRepository
    {
        ILog logger;

        public SensorRepository()
        {
            logger = LogManager.GetLogger(typeof(SensorRepository));
        }

        public bool Create(SensorCreate sensor, IMongoCollection<BsonDocument> collection)
        {
            try
            {
                var document = sensor.ToBsonDocument();
                collection.InsertOne(document, null);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("could not create sensor", ex);
                return false;
            }
        }

        public bool Delete(Sensor sensor)
        {
            try
            {
                var collection = database.GetCollection<BsonDocument>(sensor.Home);

                var result = collection.DeleteOne(Builders<BsonDocument>.Filter.Eq( "_id", ObjectId.Parse(sensor._id)), null);
                
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Could not delete sensor", ex);
                return false;
            }
        }

        public IMongoCollection<BsonDocument> GetCollectionByName(string sensorName)
        {
            try
            {
                return database.GetCollection<BsonDocument>(sensorName);
            }
            catch (Exception ex)
            {
                logger.Error("Could not get collection by name",ex);
                return null;
            }
            
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
                logger.Error("Could not insert collection to DB", ex);
                return false;
            }
            
        }

        public List<BsonDocument> GetByHome(IMongoCollection<BsonDocument> home)
        {
            try
            {
                var documents = home.Find(Builders<BsonDocument>.Filter.Empty).ToList();
                return documents;
            }
            catch (Exception ex)
            {
                logger.Error("Could not retrieve sensor by home", ex);
                return new List<BsonDocument>();
            }
        }
    }
}