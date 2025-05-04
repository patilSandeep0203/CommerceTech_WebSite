using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Subscriptions
{
    public class SubscriptionTransaction
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public int PaymentAcctId { get; set; }
        public int UserId { get; set; }
        public decimal PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime CancellationDate { get; set; }
        public decimal RefundedAmount { get; set; }
        public string StripeInvoiceId { get; set; }
        public string StripeInvoiceUrl { get; set; }
        public string StripeSubscriptionId { get; set; }
                                                   
    }
}
