using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRegistrator.Models
{
    class Home
    {
        public string Name { get; set; }
        public List<Sensor> Sensors { get; set; }

        public Home()
        {
            Sensors = new List<Sensor>();
        }

    }
}
