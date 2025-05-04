using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Locations
{
    public class LocationAddRequest
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int LocationTypeId { get; set; }
        [Required]
        public string LineOne { get; set; }
        [Required]
        public string LineTwo { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int StateId { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

    }
}
