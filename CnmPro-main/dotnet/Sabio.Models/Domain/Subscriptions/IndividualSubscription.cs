﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Subscriptions
{
    public class ActiveSubscription
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
    }
}
