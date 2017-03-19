using ICTLAB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTLAB.Repositories
{
    interface ISensorRepository
    {
        IAsyncCursor<BsonDocument> Get();
        bool Create(string sensorName);
        bool Delete(string collectionName);
        IMongoCollection<BsonDocument> GetCollectionByName(string sensorName);
        bool InsertDocumentToSensor(BsonDocument document, string sensorName);
        
    }
}
