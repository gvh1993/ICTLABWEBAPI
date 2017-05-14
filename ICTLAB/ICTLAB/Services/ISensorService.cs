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
        bool Create(CreateSensorViewModel sensor);
        bool DeleteSensor(Sensor sensor);
        Sensor GetSensorBySensorId(string id);
        bool Update(Sensor sensor);
    }
}
