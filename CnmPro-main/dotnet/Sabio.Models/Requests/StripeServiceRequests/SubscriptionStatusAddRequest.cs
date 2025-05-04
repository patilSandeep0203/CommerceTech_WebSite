using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.StripeServiceRequests
{
    public class SubscriptionStatusAddRequest
    {
        [Required]
        public int SubscriptionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime SubscriptionStartDate { get; set; }
        [Required]
        public bool isSubscribed { get; set; }
    }
}
