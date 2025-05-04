using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class QuestionsWithOptions
    {
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Question { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int QuestionTypeId { get; set; }

        public int QuestionId { get; set; }

        public int SortOrder { get; set; }

        public List<AnswerOptions> AnswerOptions { get; set; }
    }
}
