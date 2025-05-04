using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Requests.StripeServiceRequests
{
    public class SubscriptionCancelAddRequest : SubscriptionAddRequest
    {
        public DateTime CancellationDate { get; set; }
        public decimal RefundedAmount { get; set; }
        
    }
}
