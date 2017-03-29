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
    interface IHomeRepository
    {
        bool Create(string name);
        bool Delete(string name);
        IMongoCollection<BsonDocument> GetByName(string name);
        IAsyncCursor<BsonDocument> Get();
    }
}
