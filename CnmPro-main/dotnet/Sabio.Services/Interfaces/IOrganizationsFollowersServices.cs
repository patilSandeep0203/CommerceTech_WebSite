using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Organizations;


namespace Sabio.Services.Interfaces
{
    public interface IOrganizationsFollowersServices
    {
        void Add(int userId, int OrgId);

        public Paged<UserProfileBase> GetByOrganizationId(int organizationId, int pageIndex, int pageSize);

        public Paged<OrganizationBase> GetOrgByUserId(int followerId, int pageIndex, int pageSize);

        void DeleteOrgFolById(int organizationId, int followerId);
    }
}