using Sabio.Models;
using Sabio.Models.Domain;

namespace Sabio.Services.Interfaces
{
    public interface IStatesService
    {
        Paged<State> GetAll(int pageIndex, int pageSize);
    }
}
