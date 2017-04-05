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

        public List<BsonDocument> GetByHome(IMongoCollection<BsonDocument> home)
        {
            var documents = home.Find(Builders<BsonDocument>.Filter.Empty).ToList();
            return documents;
        }
    }
}