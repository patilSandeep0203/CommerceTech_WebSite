using Sabio.Models.Domain.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Surveys
{
    public class SurveySubmitAddRequest 
    {
        public int SurveyId { get; set; }

        public int StatusId { get; set; }

        public List<SurveySubmitAnswer> Questions {get; set;}
    }
}
