﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Timesheet
{
    public class TimesheetUpdateRequest: TimesheetAddRequest, IModelIdentifier
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Id { get; set; }

    }
}
