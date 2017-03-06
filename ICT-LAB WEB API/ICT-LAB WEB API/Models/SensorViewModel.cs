using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT_LAB_WEB_API.Models
{
    public class SensorViewModel
    {
        public List<Sensor> Sensors { get; set; }

        public SensorViewModel()
        {
            Sensors = new List<Sensor>();
        }
    }
}