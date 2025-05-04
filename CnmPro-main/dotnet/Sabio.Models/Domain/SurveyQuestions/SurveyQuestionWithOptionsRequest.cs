using Sabio.Models.Requests.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.SurveyQuestions
{
    public class SurveyQuestionWithOptionsRequest : SurveyQuestionAddRequest
    {
        public List<SurveyQuestionAnswerOptionAddRequest> AnswerOptions { get; set; }
    }
}
