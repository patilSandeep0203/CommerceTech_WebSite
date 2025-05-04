using Sabio.Models;
using Sabio.Models.Domain.Schedules;
using Sabio.Models.Requests.Schedules;

namespace Sabio.Services.Interfaces
{
    public interface IScheduleService
    {
        int Insert(ScheduleAddRequest model, int userId);
        Schedule GetById(int id);
        Paged<Schedule> GetByCreatedBy(int pageIndex, int pageSize, int userId);
        Paged<Schedule> GetAll(int pageIndex, int pageSize);
        void Update(ScheduleUpdateRequest model, int userId);
        void DeleteById(int id);
    }
}