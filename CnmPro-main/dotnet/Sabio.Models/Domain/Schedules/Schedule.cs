using Sabio.Models.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Schedules
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public UserProfileBase CreatedBy { get; set; }
        public UserProfileBase ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        
    }
}
