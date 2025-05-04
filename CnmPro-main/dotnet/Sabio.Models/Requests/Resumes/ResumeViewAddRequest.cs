using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Resumes
{
    public class ResumeViewAddRequest
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int ViewerId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ResumeId { get; set; }
    }
}
