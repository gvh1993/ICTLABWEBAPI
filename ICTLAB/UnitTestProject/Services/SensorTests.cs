using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICTLAB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ICTLAB.ApiControllers;
using ICTLAB.Models;
using ICTLAB.Repositories;
using MongoDB.Bson;
using Moq;


namespace ICTLAB.Services.Tests
{
    [TestClass()]
    public class SensorServiceTests
    {

        private Mock<ISensorRepository> _sensorRepository;
        private ISensorService _sensorService;

        [TestInitialize]
        public void Setup()
        {
             _sensorRepository = new Mock<ISensorRepository>();   
             _sensorService = new SensorService(_sensorRepository.Object);
        }

        [TestMethod()]
        public void ShouldReturnSensorById()
        {
            //AAA
            //Arrange
            string id = "590c9573adf1c9bb74f50d0a";
            string home = "myHome";

            Sensor sensor = new Sensor()
            {
                _id = id,
                Home = home,
                ErrorMessage = "",
                Name = "The roof",
                Type = "Temperature",
                TargetApiLink = "http://requestb.in/151q0s01",
                Unit = "Celcius",
                Room = "Hall",
                Floor = 0,
                IsActive = true,
                Readings = new List<Reading>()
            };
            sensor.Readings.Add(new Reading(){TimeStamp = new DateTime(2017, 5, 12, 22, 21, 14), Value = 23});
            sensor.Readings.Add(new Reading(){TimeStamp = new DateTime(2017, 5, 12, 22, 22, 45), Value = 23});
            sensor.Readings.Add(new Reading(){TimeStamp = new DateTime(2017, 5, 12, 22, 23, 44), Value = 23});
            sensor.Readings.Add(new Reading(){TimeStamp = new DateTime(2017, 5, 12, 22, 25, 30), Value = 23});


            //create mock document to be returned by repository
            BsonDocument document = new BsonDocument {
                {"_id", id},
                {"Home", home},
                {"ErrorMessage","" },
                {"Name","The roof" },
                {"Type", "Temperature" },
                {"TargetApiLink","http://requestb.in/151q0s01" },
                {"Unit","Celcius" },
                {"Room","Hall" },
                {"Floor",0 },
                {"IsActive", true },
                {"Readings", new BsonArray()
                {
                    new BsonDocument() { {"TimeStamp", new BsonDateTime(new DateTime(2017, 5, 12, 22, 21, 14))}, {"Value", 23} },
                    new BsonDocument() { {"TimeStamp", new BsonDateTime(new DateTime(2017, 5, 12, 22, 22, 45))}, {"Value", 23} },
                    new BsonDocument() { {"TimeStamp", new BsonDateTime(new DateTime(2017, 5, 12, 22, 23, 44))}, {"Value", 23} },
                    new BsonDocument() { {"TimeStamp", new BsonDateTime(new DateTime(2017, 5, 12, 22, 25, 30))}, {"Value", 23} }
                } }
            };

            //setup the mock document returned by GetSensorBySensorId()
            _sensorRepository.Setup(x=> x.GetSensorBySensorId(id, home)).Returns(document);

            //Act
            var result = new SensorController(_sensorService).GetSensorBySensorId(id, home) as OkNegotiatedContentResult<Sensor>;

            //Assert
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(result.Content._id, sensor._id);
            Assert.AreEqual(result.Content.Home, sensor.Home);
            Assert.AreEqual(result.Content.Name, sensor.Name);
            Assert.AreEqual(result.Content.Room, sensor.Room);
            Assert.AreEqual(result.Content.TargetApiLink, sensor.TargetApiLink);
            Assert.AreEqual(result.Content.Type, sensor.Type);
            Assert.AreEqual(result.Content.Unit, sensor.Unit);
            Assert.AreEqual(result.Content.Floor, sensor.Floor);
            Assert.AreEqual(result.Content.IsActive, sensor.IsActive);
            Assert.AreEqual(result.Content.Readings.Count, sensor.Readings.Count);
            Assert.AreEqual(result.Content.ErrorMessage, sensor.ErrorMessage);
        }


    }
}