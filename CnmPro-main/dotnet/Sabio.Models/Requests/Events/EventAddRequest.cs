using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sabio.Models.Requests.Events
{
    public class EventAddRequest
    {
        [Required]
        public int EventTypeId { get; set; }
        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public int LocationId { get; set; }
        [Required]
        public int LocationTypeId { get; set; }

        [Required]
        public string LineOne { get; set; }

        public string LineTwo { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public Double Latitude { get; set; }

        [Required]
        public Double Longitude { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [Range(0, 1)]
        public bool IsMultiDay { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public int ModifiedBy { get; set; }
    }
}
