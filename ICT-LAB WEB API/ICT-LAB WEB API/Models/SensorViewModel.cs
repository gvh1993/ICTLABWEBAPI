using System.Collections.Generic;

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