using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class SurveyInsight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstanceCount { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatorUserId { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public List<SurveyInsightDataItem> InsightData { get; set; }
    }
}
