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
        List<Sensor> GetByHome(string home);
        bool Create(SensorCreate sensor);
        bool DeleteSensorByName(Sensor sensor);
    }
}
