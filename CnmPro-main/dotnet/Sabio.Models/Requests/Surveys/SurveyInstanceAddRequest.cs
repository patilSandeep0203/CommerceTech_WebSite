using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Surveys
{
    public class SurveyInstanceAddRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SurveyId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int StatusId { get; set; }
    }
}
