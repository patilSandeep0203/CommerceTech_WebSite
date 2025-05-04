using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.OrgAdminDashboard
{
    public class JobsByActiveStatus
    {
        public int ActiveJobCount { get; set; }
        public int InactiveCount { get; set; }
    }
}
