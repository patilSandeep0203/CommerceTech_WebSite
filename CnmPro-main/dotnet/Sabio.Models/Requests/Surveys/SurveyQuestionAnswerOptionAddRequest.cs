using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Surveys
{
    public class SurveyQuestionAnswerOptionAddRequest
    {
        [StringLength(500)]
        public string Text { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
        [StringLength(200)]
        public string AdditionalInfo { get; set; }
        [Range(1, int.MaxValue)]
        public int SortOrder { get; set; }

    }
}