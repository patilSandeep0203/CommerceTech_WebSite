using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class JobSchedule
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan DailyStartTime { get; set; }
        public TimeSpan DailyEndTime { get; set; }
        public List<LookUp> DaysOfWeek { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public UserProfileBase CreatedBy { get; set; }
        public UserProfileBase ModifiedBy { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public bool JobIsActive { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
    }
}

