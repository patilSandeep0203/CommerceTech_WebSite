using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveySubmitAnswer
    {
        public int QuestionId { get; set; }

        public int AnswerOptionId { get; set; }

        public string Answer { get; set; }

        public int AnswerNumber { get; set; }
    }
}
