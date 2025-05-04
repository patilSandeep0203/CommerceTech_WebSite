using Sabio.Models.Requests.Licenses;
using Sabio.Models.Requests.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.UserProfiles
{
    public class UserProfileAddRequestV2 : LocationAddRequest
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(2)]
        public string Mi { get; set; }
        [Required]
        [StringLength(255)]
        public string AvatarUrl { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        [StringLength(10)]
        public string YearsOfExperience { get; set; }
        [Required]
        [StringLength(10)]
        public string DesiredHourlyRate { get; set; }
        [Required]
        public List<string> Profession { get; set; }
        [Required]
        public List<LicenseAddRequest> License { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}
