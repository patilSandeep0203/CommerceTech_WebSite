using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.User
{
    public class UserLogin
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [StringLength(30,MinimumLength = 8)]
        public string Email { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 8)]
        public string Password { get; set; }
    }
}
