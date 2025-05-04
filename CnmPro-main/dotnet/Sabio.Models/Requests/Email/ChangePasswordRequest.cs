using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Email
{
    public class ChangePasswordRequest
    {
        public string Token { get; set; }

        [EmailAddress(ErrorMessage = "The email was not properly formatted.")]
        [Required(ErrorMessage = "An email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }


        public int TokenType { get; set; }
    }
}
