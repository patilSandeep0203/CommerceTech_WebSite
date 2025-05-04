using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.OrgAdminDashboard
{
    public class ProposalsByOrg
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public LookUp JobType { get; set; }
        public string Occupation { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Bid { get; set; }
        public string Duration { get; set; }
        public string Details { get; set; }
        public string ProposalStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public string Email { get; set; }
        public int ModifiedBy { get; set; }
    }
}
