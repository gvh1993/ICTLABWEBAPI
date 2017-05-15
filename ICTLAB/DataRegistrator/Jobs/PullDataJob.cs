using System;
using Quartz;
using MongoDB.Driver;
using DataRegistrator.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using log4net;

namespace DataRegistrator.Jobs
{
    class PullDataJob : IJob
    {
        private readonly MongoDB.MongoDBConnector _mongoDB;
        readonly ILog _logger;
        public PullDataJob()
        {
             _mongoDB = new MongoDB.MongoDBConnector();
            _logger = LogManager.GetLogger(typeof(PullDataJob));
        }

        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("PullDataJob is executing...");
            IEnumerable<Home> homeCollection = FetchHomeAndSensorData();

            // Go through all targetapilinks and pull the data en put it properly in the mongodb database
            foreach (var home in homeCollection)
            {
                foreach (var sensor in home.Sensors)
                {
                    //try to get data from sensor
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            var content = client.DownloadString(sensor.TargetApiLink); //get data from targetapilink
                            
                            Reading reading = JsonConvert.DeserializeObject<Reading>(content); //try to parse to Reading object to make sure it is an reading object

                            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(sensor._id));
                            var update = Builders<BsonDocument>.Update
                                .Push("Readings", reading.ToBsonDocument());
                            _mongoDB.database.GetCollection<BsonDocument>(home.Name).UpdateOne(filter,update);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log errror
                        string errorMessage = "Could not contact " + sensor.TargetApiLink + " of " + sensor.Name;
                        _logger.Error(errorMessage, ex);
                        //set error in sensor and push to database
                        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(sensor._id));
                        var update = Builders<BsonDocument>.Update.Set("IsActive", false);
                            
                        _mongoDB.database.GetCollection<BsonDocument>(home.Name).UpdateOne(filter, update);

                        update = Builders<BsonDocument>.Update.Set("ErrorMessage", errorMessage);
                        _mongoDB.database.GetCollection<BsonDocument>(home.Name).UpdateOne(filter, update);
                    }
                }
            }
        }

        private IEnumerable<Home> FetchHomeAndSensorData()
        {
            List<Home> homeCollection = new List<Home>();
            
            var homes = _mongoDB.database.ListCollections().ToList();

            //iterate over collections/homes
            //iterate over the sensors per homeList of home objects
            foreach (var home in homes)
            {
                if (home["name"] == "startup_log")
                {
                    continue;
                }
                Home newHome = new Home()
                {
                    Name = home["name"].ToString()
                };

                //get IMongoCollection item
                var collection = _mongoDB.database.GetCollection<BsonDocument>(newHome.Name);
                //get sensors from the IMongoCollection
                var sensors = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();

                foreach (var sensor in sensors)
                {
                    //skip if sensor is inactive
                    if (sensor["IsActive"] == false)
                    {
                        continue;
                    }

                    newHome.Sensors.Add(new Sensor()
                    {
                        _id = sensor["_id"].ToString(),
                        Type = sensor["Type"].ToString(),
                        TargetApiLink = sensor["TargetApiLink"].ToString(),
                        Unit = sensor["Unit"].AsString,
                        IsActive = sensor["IsActive"].ToBoolean(),
                        Name = sensor["Name"].ToString()
                    });
                }
                homeCollection.Add(newHome);
            }
            return homeCollection;
        }

        /**
        * For the brave souls who get this far: You are the chosen ones,
        * the valiant knights of programming who toil away, without rest,
        * fixing our most awful code. To you, true saviors, kings of men,
        * I say this: never gonna give you up, never gonna let you down,
        * never gonna run around and desert you. Never gonna make you cry,
        * never gonna say goodbye. Never gonna tell a lie and hurt you.
        */
    }
}
