using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.ContactUs
{
    public class ContactUsAddRequest
    {
        [Required]
        [MinLength(2), MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MinLength(2), MaxLength(200)]
        public string SurName { get; set; }
        [Required]
        [MinLength(2), MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MinLength(2), MaxLength(200)]
        public string Subject { get; set; }
        [Required]
        [MinLength(10), MaxLength(4000)]
        public string Message { get; set; }

    }
}
