using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Rating;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IRatingsService
    {
        Paged<Rating> GetPage(int pageIndex, int pageSize);
        Rating Get(int id);
        RatingCreatedBy GetRatingCreatedBy(int entityId, int entityTypeId, int createdBy);
        RatingByComment GetRatingByComment(int commentId, int createdBy);
        Paged<Rating> GetByAuthor(int pageIndex, int pageSize, int createdBy);
        Paged<Rating> GetByEntityId(int pageIndex, int pageSize, int entityId, int entityTypeId);
         AvgRating GetAvgRating(int entityId, int entityTypeId);
        int Add(RatingAddRequest model, int userId);
        void Update(RatingUpdateRequest model, int userId);
        void Delete(int Id, int userId);
    }
}
