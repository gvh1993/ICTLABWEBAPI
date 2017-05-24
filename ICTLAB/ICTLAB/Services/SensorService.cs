using ICTLAB.Models;
using ICTLAB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization;


namespace ICTLAB.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        readonly log4net.ILog logger;
        public SensorService()
        {
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _sensorRepository = new SensorRepository();
        }
        public bool Create(CreateSensorViewModel sensor)
        {
            CreateSensor newSensor = new CreateSensor()
            {
                Home = sensor.Home,
                Name = sensor.Name,
                TargetApiLink = sensor.TargetApiLink,
                Type = sensor.Type.ToLower(),
                Unit = sensor.Unit,
                IsActive = true,
                Room = sensor.Room,
                Floor = sensor.Floor,
                ErrorMessage = ""
            };

            var collection = _sensorRepository.GetCollectionByName(sensor.Home);
            bool result = _sensorRepository.Create(newSensor, collection);

            return result;
        }

        public bool Update(Sensor sensor)
        {
            return _sensorRepository.Update(sensor);
        }

        public bool DeleteSensor(Sensor sensor)
        {
            //delete sensor by home
            return _sensorRepository.Delete(sensor);
        }

        public List<Sensor> GetSensorsByHome(string home)
        {
            //get collection named {{home}}
            var collection = _sensorRepository.GetCollectionByName(home);

            var result = _sensorRepository.GetSensorsByHome(collection).ToList();

            //get documents from the collection
            List<Sensor> list = new List<Sensor>();
            foreach (var document in result)
            {
                    list.Add(new Sensor()
                    {
                        _id = document["_id"].ToString(),
                        Type = document["Type"].ToString(),
                        TargetApiLink = document["TargetApiLink"].ToString(),
                        Unit = document["Unit"].AsString, Home = home,
                        IsActive = document["IsActive"].ToBoolean(),
                        Name = document["Name"].ToString(),
                        ErrorMessage = document["ErrorMessage"].ToString(),
                        Floor = document["Floor"].ToInt32(),
                        Room = document["Room"].ToString()
                    });
            }
            return list;
        }

        public Sensor GetSensorBySensorId(string id)
        {
            var sensorBson = _sensorRepository.GetSensorBySensorId(id);
            //bsondocument to sensor;

            Sensor sensor = new Sensor()
            {
                _id = sensorBson["_id"].ToString(),
                Type = sensorBson["Type"].ToString(),
                TargetApiLink = sensorBson["TargetApiLink"].ToString(),
                Unit = sensorBson["Unit"].AsString,
                IsActive = sensorBson["IsActive"].ToBoolean(),
                Name = sensorBson["Name"].ToString(),
                ErrorMessage = sensorBson["ErrorMessage"].ToString(),
                Floor = sensorBson["Floor"].ToInt32(),
                Room = sensorBson["Room"].ToString()
            };
            List<Reading> readings = (from reading in sensorBson["Readings"].AsBsonArray
                where reading["TimeStamp"] >= DateTime.Now.AddMonths(-3) //should be refactored to repository and add it to query.. but due to lack of time.. i'm sorry!
                select new Reading
                {
                    TimeStamp = reading["TimeStamp"].ToLocalTime(), Value = reading["Value"].ToDouble()
                }).ToList();
            sensor.Readings = readings;
            return sensor;
        }
    }
}