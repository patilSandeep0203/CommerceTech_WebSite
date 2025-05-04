using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveyInsightDataItem
    {
        public SurveyQuestion Question { get; set; }
        public List<SurveyInsightAnswer> Answers { get; set; }
    }
}
