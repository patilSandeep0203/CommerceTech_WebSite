using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.ShareStory
{
    public class ShareStoryAddRequest 
    {
        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Please enter valid Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Please enter valid Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string Story { get; set; }
        public List<int> FileIds { get; set; }

    }


}
