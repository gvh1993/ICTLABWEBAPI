using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ICTLAB.Models
{
    public class CreateSensorViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public string Room { get; set; }
        [Required]
        [Url]
        public string TargetApiLink { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        public string Home { get; set; }
    }
}