using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRegistrator.Models
{
    class Sensor
    {
        [BsonId]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string TargetApiLink { get; set; }
        public string Unit { get; set; }
        public string Home { get; set; }
        public List<Reading> Readings { get; set; }
        public bool IsActive { get; set; }

        public Sensor()
        {
            Readings = new List<Reading>();
        }
    }
}
