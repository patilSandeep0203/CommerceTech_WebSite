using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Schedules
{
    public class ScheduleAddRequest
    {
        [Required]
        public int OrgId { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
