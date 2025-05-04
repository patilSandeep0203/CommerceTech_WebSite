using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.SiteReferences;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface ISiteReferenceService
    {
        int Add(SiteReferenceAddRequest model);
        Paged<LookUp> SelectAll(int pageIndex, int pageSize);
        SiteRefChart SelectAllChart();
        List<LookUp> SelectAllTypes();
    }
}