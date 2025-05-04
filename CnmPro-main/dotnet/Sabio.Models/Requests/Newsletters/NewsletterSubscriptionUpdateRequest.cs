using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Newsletters
{
    public class NewsletterSubscriptionUpdateRequest
    {
        public string Email { get; set; }

        public bool IsSubscribed { get; set; }
    }
}
