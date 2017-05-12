using System;
using Quartz;
using MongoDB.Driver;
using DataRegistrator.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Net;

namespace DataRegistrator.Jobs
{
    class PullDataJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("PullDataJob is executing...");
            List<Home> homeCollection = FetchHomeAndSensorData();

            //TODO go through all targetapilinks and pull the data en put it properly in the mongodb database
            foreach (var home in homeCollection)
            {
                foreach (var sensor in home.Sensors)
                {
                    //try to get data from sensor
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            var content = client.DownloadString(sensor.TargetApiLink);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    //if fails set error
                }
            }
        }

        private List<Home> FetchHomeAndSensorData()
        {
            List<Home> homeCollection = new List<Home>();
            MongoDB.MongoDBConnector mongoDB = new MongoDB.MongoDBConnector();
            var homes = mongoDB.database.ListCollections().ToList();

            //iterate over collections/homes
            //iterate over the sensors per homeList of home objects
            foreach (var home in homes)
            {
                Home newHome = new Home()
                {
                    Name = home["name"].ToString()
                };

                //get IMongoCollection item
                var collection = mongoDB.database.GetCollection<BsonDocument>(newHome.Name);
                //get sensors from the IMongoCollection
                var sensors = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();

                foreach (var sensor in sensors)
                {
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
    }
}
