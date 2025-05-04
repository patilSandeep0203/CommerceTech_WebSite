using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveyWithDetails : Survey
    {
        public List<QuestionsWithOptions> QandA { get; set; }
    }
}
