using Sabio.Models;
using Sabio.Models.Domain.Resource;
using Sabio.Models.Requests.Resources;

namespace Sabio.Services.Interfaces
{
    public interface IResourcesService
    {
        public Resource GetById(int id);
        public Paged<Resource> GetResourcesByCreatedBy(int userId, int pageIndex, int pageSize);
        public Paged<Resource> GetResourcesByResourceCategoryId(int resourceId, int pageIndex, int pageSize);
        public Paged<Resource> GetAllResources(int pageIndex, int pageSize);
        public ResourceDetails GetResourceByDetailsId(int id);
        public Paged<ResourceDetails> GetResourcesByDetailsLocationTypeId(int locationTypeId, int pageIndex, int pageSize);
        public Paged<ResourceDetails> SearchDetails(int pageIndex, int pageSize, string query);
        public int Create(ResourceAddRequest model, int userId);
        public void Update(ResourceUpdateRequest model, int userId);
        public void Delete(int id);
    }
}