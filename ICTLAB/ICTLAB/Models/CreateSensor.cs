using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICTLAB.Models
{
    public class CreateSensor
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string TargetApiLink { get; set; }
        public string Unit { get; set; }
        public string Home { get; set; }
        public int Floor { get; set; }
        public string Room { get; set; }
        public List<Reading> Readings { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }

        public CreateSensor()
        {
            Readings = new List<Reading>();
        }
    }
}