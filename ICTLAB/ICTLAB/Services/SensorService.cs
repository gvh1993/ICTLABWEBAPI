using ICTLAB.Models;
using ICTLAB.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


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
                IsActive = true
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
                        Name = document["Name"].ToString()
                    });
            }
            return list;
        }

        public Sensor GetSensorBySensorId(Sensor sensor)
        {
            return null;
        }
    }
}