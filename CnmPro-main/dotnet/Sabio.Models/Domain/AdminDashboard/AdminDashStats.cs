using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.AdminDashboard
{
    public class AdminDashStats
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalOrg { get; set; }
        public decimal UsersGrowth { get; set; }
        public decimal ActiveUsersGrowth { get; set; }
        public decimal OrgGrowth { get; set; }
    }
}
