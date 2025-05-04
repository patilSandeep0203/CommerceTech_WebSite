using Sabio.Models;
using Sabio.Models.Domain;

namespace Sabio.Services.Interfaces
{
    public interface ILocationsService
    {
        Paged<Location> GetAll(int pageIndex, int pageSize);
    }
}
