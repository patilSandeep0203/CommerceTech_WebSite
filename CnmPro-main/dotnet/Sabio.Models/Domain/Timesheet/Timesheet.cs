using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Timesheet
{
    public class Timesheet : UserJob
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string Comment { get; set; }
        public int LocationId { get; set; }
        public string City { get; set; }
        public int TotalHrs { get; set; }
    }
}