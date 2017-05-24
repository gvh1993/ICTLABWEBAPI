using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ICTLAB.Models
{
    public class Sensor
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
        public string ErrorMessage { get; set; }
        public string Room { get; set; }
        public int Floor { get; set; }

        public Sensor()
        {
            Readings = new List<Reading>();
        }
    }
    
}