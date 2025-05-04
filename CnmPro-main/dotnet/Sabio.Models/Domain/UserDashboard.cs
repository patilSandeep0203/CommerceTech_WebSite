using Sabio.Models.Domain.Jobs;
using Sabio.Models.Domain.Newsletters;
using Sabio.Models.Domain.Timesheet;
using Sabio.Models.Domain.Resumes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class UserDashboard
    {
        public Blog Blog { get; set; }
        public List<Newsletter> Newsletters { get; set; }
        public List<BaseTimesheet> Timesheets { get; set; }
        public List<OrganizationStats> OrgStats { get; set; }
        public List<JobBase> Jobs { get; set; }
    }
}
