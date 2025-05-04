using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests
{
    public class OrganizationAddRequest
    {
        [Required]
        public int LocationTypeId { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public int StateId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [Required]
        public int OrganizationTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string SiteUrl { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
