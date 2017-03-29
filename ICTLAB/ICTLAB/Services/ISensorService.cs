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
        List<Sensor> Get();
        List<Sensor> GetByHome(string home);
        bool Create(Sensor sensor);
        bool DeleteSensorByName(string sensorName);
    }
}
