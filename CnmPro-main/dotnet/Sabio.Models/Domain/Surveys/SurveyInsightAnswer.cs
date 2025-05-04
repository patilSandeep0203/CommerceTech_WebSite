using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveyInsightAnswer
    {
        public int? OptionId { get; set; }
        public string AnswerString { get; set; }
        public int? AnswerBinary { get; set; }
        public int? Total { get; set; }
    }
}
