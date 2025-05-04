using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Timesheet
{
    public class BaseTimesheet
    {
        public DateTime Date { get; set; }
        public int TotalHrs { get; set; }
        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}
