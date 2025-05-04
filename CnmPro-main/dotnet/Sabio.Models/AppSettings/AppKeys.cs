using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.AppSettings
{
    public class AppKeys
    {
        public string SendGridAppKey { get; set; }
        public string StripeConfigurationApiKey { get; set; }
        public string StripeWebhookApiKey { get; set; }
        public string DomainEmail { get; set; }
        public string Url { get; set; }
        public string HttpsUrl { get; set; }
        public string TwilioAccountSid { get; set; }
        public string TwilioAuthToken { get; set; }

    }
}
