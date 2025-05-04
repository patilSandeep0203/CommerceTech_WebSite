using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sabio.Models.AppSettings;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Stripe;
using Stripe.Checkout;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sabio.Web.Api.Controllers.StripeController
{
    [Route("api/stripe")]
    [ApiController]
    public class StripeWebhookApiController : BaseApiController
    {
        private AppKeys _appKeys;
        private IAuthenticationService<int> _authService = null;
        private Domain _domain;
        private IStripeService _service = null;
        public StripeWebhookApiController(IOptions<AppKeys> appKeys,
            IOptions<Domain> domain, 
            IStripeService service, 
            ILogger<StripeWebhookApiController> logger, 
            IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
            _appKeys = appKeys.Value;
            _domain = domain.Value;
        }

        [Route("management")]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            String json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;
            string endpointSecret = _appKeys.StripeWebhookApiKey;
            Event stripeEvent;
            try
            {
                string webhookSecret = endpointSecret;
                stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], webhookSecret);
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error occurred: {ex}");
                return BadRequest();
            }
            switch (stripeEvent.Type)
            {
                case "checkout.session.completed":
                    Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
                    break;
                case "checkout.session.expired":
                    Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
                    break;
                case "checkout.session.failed":
                    Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
                    break;
                case "invoice.paid":
                    Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
                    break;

            }
            return Ok();
        }

    }

}
