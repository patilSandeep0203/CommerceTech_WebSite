using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.StripeServiceRequests
{
    public class SubscriptionStatusCancelRequest 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime SubscriptionEndDate { get; set; }
        [Required]
        public bool isSubscribed { get; set; }
    }
}
