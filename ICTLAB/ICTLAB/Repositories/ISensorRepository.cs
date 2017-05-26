using ICTLAB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ICTLAB.Repositories
{
    interface ISensorRepository
    {
        List<BsonDocument> GetSensorsByHome(IMongoCollection<BsonDocument> collection);
        bool Create(CreateSensor sensor, IMongoCollection<BsonDocument> collection);
        bool Delete(Sensor sensor);
        IMongoCollection<BsonDocument> GetCollectionByName(string sensorName);
        BsonDocument GetSensorBySensorId(string id, string home);
        List<BsonDocument> GetSensorsByType(CreateSensor sensor);
        bool Update(Sensor sensor);
    }
}
