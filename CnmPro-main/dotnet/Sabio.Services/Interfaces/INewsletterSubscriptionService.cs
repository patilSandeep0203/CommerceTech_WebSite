using Sabio.Models;
using Sabio.Models.Domain.Newsletters;
using Sabio.Models.Requests.Newsletters;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface INewsletterSubscriptionService
    {
        void Add(NewsletterSubscriptionAddRequest model);
        void Update(NewsletterSubscriptionUpdateRequest model);

        List<NewsletterSubscription> GetAllSubscribed();
        Paged<NewsletterSubscription> GetAllByPage(int pageIndex, int pageSize);
    }
}
