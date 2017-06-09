using ICTLAB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ICTLAB.Repositories
{
    public interface ISensorRepository
    {
        IEnumerable<BsonDocument> GetSensorsByHome(IMongoCollection<BsonDocument> collection);
        IEnumerable<BsonDocument> GetSensorsWithoutCurrent(IMongoCollection<BsonDocument> collection, Sensor sensor);
        bool Create(CreateSensor sensor, IMongoCollection<BsonDocument> collection);
        bool Delete(Sensor sensor);
        IMongoCollection<BsonDocument> GetCollectionByName(string sensorName);
        BsonDocument GetSensorBySensorId(string id, string home);
        bool Update(Sensor sensor);
    }
}
