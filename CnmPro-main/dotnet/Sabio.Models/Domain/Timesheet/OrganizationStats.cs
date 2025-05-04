using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Timesheet
{
    public class OrganizationStats
    {
        public string Name { get; set; }
        public int OrgId { get; set; }
        public int Total { get; set; }
    }
}
