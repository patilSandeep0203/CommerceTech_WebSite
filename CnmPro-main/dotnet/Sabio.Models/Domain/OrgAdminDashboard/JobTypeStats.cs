using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.OrgAdminDashboard
{
    public class JobTypeStats
    {
        public int PartTimeCount { get; set; }
        public int FullTimeCount { get; set; }
        public int ContractCount { get; set; }
    }
}
