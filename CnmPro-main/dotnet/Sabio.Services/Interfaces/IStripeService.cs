using Microsoft.AspNetCore.Mvc;
using Sabio.Models;
using Sabio.Models.Domain.Subscriptions;
using Sabio.Models.Requests.StripeServiceRequests;
using Stripe;
using Stripe.Checkout;

namespace Sabio.Services.Interfaces
{
    public interface IStripeService
    {
         StripeList<Customer> GetCustomers();
         StripeList<Product> GetProduct();
        Invoice GetInvoice(string invoiceId);
        Price GetPrice(string priceId);
        Customer GetCustomer(string userId);

        Subscription GetSubscription(string subId);
        Subscription CreateSubscription(string customerId, string priceId);

        int AddPurchase(SubscriptionBuyAddRequest model, int id);
        int AddCancellation(SubscriptionCancelAddRequest model, int id);
        int addSubscriptionStatus(SubscriptionStatusAddRequest model, int id);
        void UpgradeSubscriptionStatus(SubscriptionStatusUpdateRequest model, int id);
        void CancelSubscriptionStatus(SubscriptionStatusCancelRequest model, int id);
        Paged<ActiveSubscription> SelectAll(int pageIndex, int pageSize);
        SubscriptionStatus GetSubscriptionStatusById(int userId);
        Paged<ActiveSubscription> GetActiveSubscriptions(int pageIndex, int pageSize);


    }
}
