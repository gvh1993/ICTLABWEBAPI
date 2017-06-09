using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICTLAB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICTLAB.ApiControllers;
using ICTLAB.Repositories;
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
             //_sensorService = new SensorService();
        }

        [TestMethod()]
        public void ShouldReturnSensorById()
        {
            //AAA
            //Arrange
            //SensorController controller = new SensorController();
            string id = "590c9573adf1c9bb74f50d0a";
            string home = "myHome";
            //Act
            //var result = controller.GetSensorBySensorId(id, home);
            //Assert
            Assert.Fail();
        }
    }
}