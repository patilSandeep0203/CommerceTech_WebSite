using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Resumes
{
    public class ResumeAddRequest
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(1,999)]
        public int ResumeContactId { get; set; }

        [Required]
        [Range(1, 999)]
        public int ResumeFileId { get; set; }
    }
}
