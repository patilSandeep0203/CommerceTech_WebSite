using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Requests.StripeServiceRequests

{
    public class SubscriptionBuyAddRequest : SubscriptionAddRequest
    {
        public decimal PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
