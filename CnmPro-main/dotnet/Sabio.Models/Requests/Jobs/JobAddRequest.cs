using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Jobs
{
    public class JobAddRequest 
    {
        [Required]
        public int JobTypeId { get; set; }
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
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(3000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(3000)]
        public string Requirements { get; set; }
        [Required]
        [Range(0, 1)]
        public bool IsActive { get; set; }
        [Required]
        [MaxLength(50)]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone Number")]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactEmail { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
