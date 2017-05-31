using ICTLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTLAB.Services
{
    interface ISensorService
    {
        List<Sensor> GetSensorsByHome(string home);
        List<Sensor> GetSensorsWithoutReadingsByHome(string home);
        bool Create(CreateSensorViewModel sensor);
        bool DeleteSensor(Sensor sensor);
        Sensor GetSensorBySensorId(string id, string home);
        
        bool Update(Sensor sensor);
    }
}
