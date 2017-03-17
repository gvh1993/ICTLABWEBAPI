using System.Collections.Generic;

namespace Dashboard.Models
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