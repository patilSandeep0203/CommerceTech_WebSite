using Sabio.Models.Domain;
using Sabio.Models.Domain.Schedules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Schedules
{
    public class JobScheduleAddRequest
    {
        [Required]
        public int OrgId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public TimeSpan DailyStartTime { get; set; }
        [Required]
        public TimeSpan DailyEndTime { get; set; }
        public List<string> DaysOfWeek { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        [Required]
        public int JobId { get; set; }
    }
}

