using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveyAnswers
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int InstancId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int QuestionId { get; set; }
        
        public int AnswerOptionId { get; set; }
        
        public string Answer { get; set; }
        
        public int AnswerNumber { get; set; }
        
        [Required]
        public DateTime DateCreated { get; set; }
        
        [Required]
        public DateTime DateModified { get; set; }
    }
}
