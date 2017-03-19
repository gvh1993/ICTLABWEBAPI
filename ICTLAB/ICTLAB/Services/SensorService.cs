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
        public string CreateFirstAvailableSensorName(Sensor sensor)
        {
            string sensorName = "";
            bool foundUnique = false;
            int count = 0;

            while (!foundUnique)
            {
                sensorName = sensor.Type + count;
                if (sensorRepository.Create(sensorName))
                {
                    BsonDocument document = new BsonDocument().AddRange(sensor.ToBsonDocument());

                    var collection = sensorRepository.GetCollectionByName(sensorName);
                    sensorRepository.InsertDocumentToSensor(document, sensorName);

                    foundUnique = true;
                }
                else
                {
                    count++;
                }
            }

            return sensorName;
        }

        public bool DeleteSensorByName(string sensorName)
        {
            return sensorRepository.Delete(sensorName);
        }

        public List<Sensor> Get()
        {
            List<Sensor> sensors = new List<Sensor>();

            var collections = sensorRepository.Get().ToList();

            foreach (var collection in collections)
            {
                Sensor sensor = new Sensor()
                {
                    Name = collection["name"].AsString
                };
                if (sensor.Name != "system.version" && sensor.Name != "startup_log")
                {
                    sensors.Add(sensor);
                }
            }
            return sensors;
        }
    }
}