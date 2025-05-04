using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Subscriptions
{
    public class SubscriptionStatus
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public bool isSubscribed { get; set; }
    }
}
