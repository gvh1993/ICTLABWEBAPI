using ICTLAB.Models;
using ICTLAB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;


namespace ICTLAB.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        readonly log4net.ILog logger;
        public SensorService(ISensorRepository sensorRepository)
        {
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //_sensorRepository = new SensorRepository();
            _sensorRepository = sensorRepository;
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
            if (collection == null)
                return false;

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

        public List<Sensor> GetSensorsWithoutCurrent(Sensor currentSensor)
        {
            //get collection named {{home}}
            var collection = _sensorRepository.GetCollectionByName(currentSensor.Home);
            if (collection == null)
                return new List<Sensor>();

            var result = _sensorRepository.GetSensorsWithoutCurrent(collection, currentSensor).ToList();

            //get documents from the collection
            List<Sensor> list = new List<Sensor>();
            foreach (var document in result)
            {
                Sensor sensor = new Sensor()
                {
                    _id = document["_id"].ToString(),
                    Type = document["Type"].ToString(),
                    TargetApiLink = document["TargetApiLink"].ToString(),
                    Unit = document["Unit"].AsString,
                    Home = currentSensor.Home,
                    IsActive = document["IsActive"].ToBoolean(),
                    Name = document["Name"].ToString(),
                    ErrorMessage = document["ErrorMessage"].ToString(),
                    Floor = document["Floor"].ToInt32(),
                    Room = document["Room"].ToString(),
                };
                List<Reading> readings = new List<Reading>();
                foreach (var reading in document["Readings"].AsBsonArray)
                {
                    readings.Add(new Reading
                    {
                        TimeStamp = reading["TimeStamp"].ToUniversalTime(),
                        Value = reading["Value"].ToDouble()
                    });
                }
                sensor.Readings = readings;
                list.Add(sensor);
            }
            return list;
        }

        public List<Sensor> GetSensorsWithoutReadingsByHome(string home)
        {
            //get collection named {{home}}
            var collection = _sensorRepository.GetCollectionByName(home);
            if (collection == null)
                return new List<Sensor>();

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
                    Unit = document["Unit"].AsString,
                    Home = home,
                    IsActive = document["IsActive"].ToBoolean(),
                    Name = document["Name"].ToString(),
                    ErrorMessage = document["ErrorMessage"].ToString(),
                    Floor = document["Floor"].ToInt32(),
                    Room = document["Room"].ToString()
                });
            }
            return list;
        }

        public Sensor GetSensorBySensorId(string id, string home)
        {
            var sensorBson = _sensorRepository.GetSensorBySensorId(id, home);
            //bsondocument to sensor;

            Sensor sensor = new Sensor()
            {
                _id = sensorBson["_id"].ToString(),
                Type = sensorBson["Type"].ToString(),
                TargetApiLink = sensorBson["TargetApiLink"].ToString(),
                Unit = sensorBson["Unit"].AsString,
                IsActive = sensorBson["IsActive"].ToBoolean(),
                Name = sensorBson["Name"].ToString(),
                Home = sensorBson["Home"].ToString(),
                ErrorMessage = sensorBson["ErrorMessage"].ToString(),
                Floor = sensorBson["Floor"].ToInt32(),
                Room = sensorBson["Room"].ToString()
            };
            List<Reading> readings = new List<Reading>();
            foreach (var reading in sensorBson["Readings"].AsBsonArray)
                readings.Add(new Reading
                {
                    TimeStamp = reading["TimeStamp"].ToLocalTime(),
                    Value = reading["Value"].ToDouble()
                });
            sensor.Readings = readings;
            return sensor;
        }
    }
}