using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.User
{
    public class UserUpdatePasswordRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int Id { get; set; }
        public string Token { get; set; }

        [EmailAddress(ErrorMessage = "The email was not properly formatted.")]
        [Required(ErrorMessage = "An email is required.")]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 8,
           ErrorMessage = "The password should be between 8 and 50 characters. ")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
           ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit and one special character.")]
        [Required(ErrorMessage = "A password is required.")]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 8,
   ErrorMessage = "The password should be between 8 and 50 characters. ")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
   ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit and one special character.")]
        [Required(ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
