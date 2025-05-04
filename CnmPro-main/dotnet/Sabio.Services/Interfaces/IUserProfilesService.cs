using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Licenses;
using Sabio.Models.Requests.UserProfiles;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IUserProfilesService
    {
        void DeleteById(int Id);
        UserProfile GetByCurrentId(int Id);
        Paged<UserProfile> GetAll(int pageInex, int pageSize);
        Paged<UserProfile> Pagination(int PageIndex, int PageSize, int CreatedBy);
        int AddV2(UserProfileAddRequestV2 model, int userId);
        void UpdateV2(UserProfileUpdateRequestV2 model, int userId);
        UserDashboard GetDashboard(int userId);
    }
}
