using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveyInstance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string CompanyLogo { get; set; }
        public LookUp SurveyType { get; set; }
        public LookUp InstanceStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public List<SurveyDataItem> SurveyData { get; set; }

    }
}
