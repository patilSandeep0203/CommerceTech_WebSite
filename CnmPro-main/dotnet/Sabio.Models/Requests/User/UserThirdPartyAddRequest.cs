using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.User
{
    public class UserThirdPartyAddRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(10)]
        public string Token { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }

    }
}
