using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Timesheet
{
    public class UserJob: UserOrganization
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string Title { get; set; }
    }
}
