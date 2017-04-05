using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICTLAB.Models
{
    public class SensorCreate
    {
        public string Type { get; set; }
        public string TargetApiLink { get; set; }
        public string Name { get; set; }
        public string Home { get; set; }
    }
}