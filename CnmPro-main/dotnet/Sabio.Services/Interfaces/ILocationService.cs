using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Locations;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface ILocationService
    {
        Location GetById(int id);
        Paged<Location> GetAll(int pageIndex, int pageSize);
        Paged<Location> GetByAuthor(int authorId, int pageIndex, int pageSize);
        List<Location> GetByGeo(double lat, double lng, int distance);
        int Add(LocationAddRequest model, int userId);
        void Update(LocationUpdateRequest model, int userId);
        void Delete(int id);
    }
}