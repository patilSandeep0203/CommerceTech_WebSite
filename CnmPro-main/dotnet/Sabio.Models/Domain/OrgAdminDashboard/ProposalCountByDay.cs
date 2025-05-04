using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.OrgAdminDashboard
{
    public class ProposalCountByDay
    {
        public DateTime DateCreated { get; set; }

        public int Count { get; set; }
    }
}
