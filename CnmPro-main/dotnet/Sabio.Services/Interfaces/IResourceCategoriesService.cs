using Sabio.Models.Domain;
using Sabio.Models.Requests.ResourceCategories;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IResourceCategoriesService
    {
        List<LookUp> GetAll();
        int Create(ResourceCategoryAddRequest model);
        void Update(ResourceCategoryUpdateRequest model);
    }
}