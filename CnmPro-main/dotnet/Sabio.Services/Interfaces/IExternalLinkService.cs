using Sabio.Models.Domain;
using Sabio.Models.Requests.ExternalLinks;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IExternalLinkService
    {
        int Add(ExternalLinkAddRequest model, int userId);
        void Delete(int id, int userId);
        List<ExternalLink> GetByUserId(int userId);
        void Update(ExternalLinkUpdateRequest model, int userId);
    }
}
