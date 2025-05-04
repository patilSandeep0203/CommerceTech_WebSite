using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Resumes
{
    public class ResumeUpdateRequest : ResumeAddRequest, IModelIdentifier
    {
        [Required]
        [Range(1, 999)]
        public int Id { get; set; }
    }
}
