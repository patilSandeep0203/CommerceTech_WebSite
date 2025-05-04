using Sabio.Models.Domain.Timesheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Timesheet
{
    public class TimesheetAddRequest
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(10)]
        public string TimeFrom { get; set; }
        [Required]
        [StringLength(10)]
        public string TimeTo { get; set; }
        [StringLength(800)]
        public string Comment { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int OrganizationId { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int JobId { get; set; }
    }
}
