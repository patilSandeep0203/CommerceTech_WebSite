﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Events
{
    public class EventSearchResult : Event
    {
        public double Distance { get; set; }
    }
}
