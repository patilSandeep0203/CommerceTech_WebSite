using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sabio.Models;
using Sabio.Models.AppSettings;
using Sabio.Models.Domain.Subscriptions;
using Sabio.Models.Requests.StripeServiceRequests;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sabio.Web.Api.Controllers.StripeController
{
    [Route("api/subscription")]
    [ApiController]
    public class StripeApiController : BaseApiController
    {
        private AppKeys _appKeys;
        private IAuthenticationService<int> _authService = null;
        private Domain _domain;
        private IStripeService _service = null;

        public StripeApiController(IOptions<AppKeys> appKeys,
            IOptions<Domain> domain,
            IStripeService service,
            ILogger<StripeApiController> logger,
            IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
            _appKeys = appKeys.Value;
            _domain = domain.Value;
        }

        [HttpGet("products")]
        public ActionResult<ItemResponse<Product>> GetProducts()
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {
                StripeList<Product> products = _service.GetProduct();
                if (products == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found.");
                }
                else
                {
                    response = new ItemResponse<StripeList<Product>> { Item = products };

                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpPost("products")]
        public ActionResult<ItemResponse<Price>> GetProductPrice(string priceId)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {
                if (priceId == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found.");
                }
                else
                {
                    Price price = _service.GetPrice(priceId);
                    response = new ItemResponse<Price> { Item = price };

                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<string>> CreateSession(string priceId)
        {
            int responseCode = 200;
            BaseResponse response = null;

            if (priceId == null)
            {
                responseCode = 404;
                response = new ErrorResponse("Stripe Resource not Found, Possible Product Error");
                return StatusCode(responseCode, response);
            }
            else
            {
                try
                {
                    StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

                    String Domain = _domain.HttpsUrl;

                    string CheckoutUrl = Domain + "/subscriptions/successPg";

                    SessionCreateOptions options = new SessionCreateOptions
                    {

                        LineItems = new List<SessionLineItemOptions>
                            {
                                new SessionLineItemOptions
                                {
                                    Price = $"{priceId}",
                                    Quantity = 1,
                                },
                            },

                        Mode = "subscription",
                        SuccessUrl = CheckoutUrl + "?session_id={CHECKOUT_SESSION_ID}",
                        CancelUrl = CheckoutUrl + "?canceled",
                    };
                    SessionService service = new SessionService();
                    Session session = await service.CreateAsync(options);

                    return session.Id;
                }
                catch (Exception ex)
                {
                    response = new ErrorResponse(ex.Message);
                    responseCode = 500;
                    base.Logger.LogError(ex.ToString());
                    return StatusCode(responseCode, response);
                }
            }
        }

        [HttpGet("order/success")]
        public ActionResult OrderSuccess(string session_id)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {
                if (session_id == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found.");
                }
                else
                {
                    StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

                    SessionService sessionService = new SessionService();
                    Session session = sessionService.Get(session_id);

                    CustomerService customerService = new CustomerService();
                    Customer customer = customerService.Get(session.CustomerId);

                    SubscriptionService subscriptionService = new SubscriptionService();
                    Subscription subscription = subscriptionService.Get(session.SubscriptionId);

                    InvoiceService invoiceService = new InvoiceService();
                    Invoice invoice = invoiceService.Get(subscription.LatestInvoiceId);

                    response = new ItemResponse<Invoice> { Item = invoice };

                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpPost("customer/portal")]
        public async Task<ActionResult<string>> CustomerPortal(string customerId)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {

                if (customerId == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found.");
                    return StatusCode(responseCode, response);
                }
                else
                {
                    StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;
                    String Domain = _domain.HttpsUrl;

                    string CheckoutUrl = Domain + "/subscriptions";

                    var options = new Stripe.BillingPortal.SessionCreateOptions
                    {
                        Customer = $"{customerId}",
                        ReturnUrl = CheckoutUrl,
                    };
                    Stripe.BillingPortal.SessionService service = new Stripe.BillingPortal.SessionService();
                    Stripe.BillingPortal.Session session = await service.CreateAsync(options);

                    return session.Url;
                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
                return StatusCode(responseCode, response);
            }


        }

        [HttpPost("customers")]
        public ActionResult<string> CreateCustomer(string userId)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {

                if (userId == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found, Possible Product Error");
                }
                else
                {
                    StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

                    String Domain = _domain.HttpsUrl;

                    string ReturnUrl = Domain + "/checkoutForm";

                    var options = new CustomerCreateOptions
                    {
                        Email = $"{userId}",
                        Description = "My First Test Customer (created for API docs at https://www.stripe.com/docs/api)",
                    };
                    var service = new CustomerService();
                    Customer customer = service.Create(options);

                    return customer.Id;

                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpGet("customers")]
        public ActionResult<ItemResponse<string>> GetCustomer(string userId)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {
                Customer customer = _service.GetCustomer(userId);
                if (customer == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found.");
                }
                else
                {
                    response = new ItemResponse<string> { Item = customer.Id };

                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpGet("invoice")]
        public ActionResult<ItemResponse<PaymentIntent>> GetInvoice(string invoiceId)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {
                Invoice invoice = _service.GetInvoice(invoiceId);
                if (invoice == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found.");
                }
                else
                {
                    response = new ItemResponse<Invoice> { Item = invoice };
                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpPost("invoice")]
        public ActionResult<string> PostInvoice(string invoiceId, string userId)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {

                if (userId == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found, Possible Product Error");
                }
                else
                {
                    StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

                    String Domain = _domain.HttpsUrl;

                    string ReturnUrl = Domain + "/checkoutForm";

                    var options = new CustomerCreateOptions
                    {
                        Email = $"{userId}",
                        Description = "My First Test Customer (created for API docs at https://www.stripe.com/docs/api)",
                    };
                    var service = new CustomerService();
                    Customer customer = service.Create(options);

                    return customer.Id;

                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpGet("subscription/info")]
        public ActionResult<ItemResponse<Subscription>> GetSubscription(string subId)
        {
            int responseCode = 200;
            BaseResponse response = null;
            try
            {
                Subscription subscription = _service.GetSubscription(subId);
                if (subscription == null)
                {
                    responseCode = 404;
                    response = new ErrorResponse("Stripe Resource not Found.");
                }
                else
                {
                    response = new ItemResponse<Subscription> { Item = subscription };
                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse(ex.Message);
                responseCode = 500;
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(responseCode, response);
        }

        [HttpGet("admin/status/active")]
        public ActionResult<ItemResponse<Paged<ActiveSubscription>>> GetActiveSubscriptions(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<ActiveSubscription> paged = _service.GetActiveSubscriptions(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<ActiveSubscription>> response = new ItemResponse<Paged<ActiveSubscription>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }
        
        [HttpGet("admin/status/{id:int}")]
        public ActionResult<ItemResponse<SubscriptionStatus>> GetSubscriptionStatusById(int userId)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                SubscriptionStatus status = _service.GetSubscriptionStatusById(userId);
                if (status == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<SubscriptionStatus> { Item = status };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse("$Generic Error: ${ex.Message}");
            }
            return StatusCode(iCode, response);
        }
        
        [HttpGet("admin/status/all")]
        public ActionResult<ItemResponse<Paged<ActiveSubscription>>> SelectAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;
            try
            {
                Paged<ActiveSubscription> paged = _service.SelectAll(pageIndex, pageSize);
                if (paged == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<ActiveSubscription>> response = new ItemResponse<Paged<ActiveSubscription>>();
                    response.Item = paged;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }
        
        [HttpPost("admin/purchase")]
        public ActionResult<ItemResponse<int>> Purchase(SubscriptionBuyAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;

            try
            {
                int id = _service.AddPurchase(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);

            }

            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }
        
        [HttpPost("admin/cancel")]
        public ActionResult<ItemResponse<int>> Cancellation(SubscriptionCancelAddRequest model)
        {
            int userId = _authService.GetCurrentUserId();

            ObjectResult result = null;

            try
            {
                int id = _service.AddCancellation(model, userId);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);

            }

            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }
    }
}
