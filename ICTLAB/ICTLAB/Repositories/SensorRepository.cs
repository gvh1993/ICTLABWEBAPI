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
        readonly ILog _logger;

        public SensorRepository()
        {
            _logger = LogManager.GetLogger(typeof(SensorRepository));
        }

        public bool Create(CreateSensor sensor, IMongoCollection<BsonDocument> collection)
        {
            try
            {
                var document = sensor.ToBsonDocument();
                collection.InsertOne(document, null);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("could not create sensor", ex);
                return false;
            }
        }

        public bool Update(Sensor sensor)
        {
            try
            {
                var collection = database.GetCollection<BsonDocument>(sensor.Home);

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(sensor._id));
                var update = Builders<BsonDocument>.Update.Set("IsActive", sensor.IsActive);
                collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Could not update IsActive", ex);
                return false;
            }
        }

        public bool Delete(Sensor sensor)
        {
            try
            {
                var collection = database.GetCollection<BsonDocument>(sensor.Home);

                collection.DeleteOne(Builders<BsonDocument>.Filter.Eq( "_id", ObjectId.Parse(sensor._id)), null);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Could not delete sensor", ex);
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
                _logger.Error("Could not get collection by name",ex);
                return null;
            }
            
        }

        public List<BsonDocument> GetSensorsByHome(IMongoCollection<BsonDocument> home)
        {
            try
            {
                var documents = home.Find(Builders<BsonDocument>.Filter.Empty).ToList();
                return documents;
            }
            catch (Exception ex)
            {
                _logger.Error("Could not retrieve sensor by home", ex);
                return new List<BsonDocument>();
            }
        }

        public BsonDocument GetSensorBySensorId(Sensor sensor)
        {
            return null;
        }

        public List<BsonDocument> GetSensorsByType(CreateSensor sensor )
        {
            try
            {
                var home = database.GetCollection<BsonDocument>(sensor.Home);
                var documents = home.Find(Builders<BsonDocument>.Filter.Eq("Type", sensor.Type)).ToList();

                return documents;
            }
            catch (Exception ex)
            {
                _logger.Error("Could not retrieve sensor by type", ex);
                return new List<BsonDocument>();
            }

        }
    }
}