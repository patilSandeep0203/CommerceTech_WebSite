using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Requests.StripeServiceRequests
{
    public class SubscriptionAddRequest
    {
        [Required]
        public int SubscriptionId { get; set; }
        [Required]
        public int PaymentAcctId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string StripeInvoiceId { get; set; }
        [Required]
        public string StripeInvoiceUrl { get; set; }
        [Required]
        public string StripeSubscriptionId { get; set; }
    }
}
