using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class AnswerOptions
    {
        public int AnsOptId { get; set; }

        public string Text { get; set; }

        public string Value { get; set; }

        public string AdditionalInfo { get; set; }
        
        public int SortOrder { get; set; }
    }
}
