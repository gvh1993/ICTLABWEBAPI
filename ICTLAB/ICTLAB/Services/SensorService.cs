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
        private ISensorRepository sensorRepository;
        readonly log4net.ILog logger;
        public SensorService()
        {
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            sensorRepository = new SensorRepository();
        }
        public bool Create(Sensor sensor)
        {
            
            var collection = sensorRepository.GetCollectionByName(sensor.Home);
            bool result = sensorRepository.Create(sensor, collection);

            return result;
        }

        public bool DeleteSensorByName(string sensorName)
        {
            return sensorRepository.Delete(sensorName);
        }

        public List<Sensor> Get()
        {
            throw new NotImplementedException();
        }
        public List<Sensor> GetByHome(string home)
        {
            List<Sensor> sensors = new List<Sensor>();

            //get collection named {{home}}
            var collection = sensorRepository.GetCollectionByName(home);

            var result = sensorRepository.GetByHome(collection).ToList();

            foreach (var document in result)
            {
                Sensor sensor = new Sensor()
                {
                    //_id = document["_id"].AsObjectId.ToString(),
                    Name = document["Name"].AsString,
                    Type = document["Type"].AsString,
                    TargetApiLink = document["TargetApiLink"].AsString
                };
                sensors.Add(sensor);
            }
            //get documents from the collection
            return sensors;
        }
    }
}