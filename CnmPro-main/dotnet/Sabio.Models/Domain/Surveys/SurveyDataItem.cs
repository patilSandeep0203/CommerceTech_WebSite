using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveyDataItem
    {
        public List<SurveyQuestion> Question { get; set; }
        public int BinaryAnswer { get; set; }
        public string OpenEndedAnswer { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
    }
}
