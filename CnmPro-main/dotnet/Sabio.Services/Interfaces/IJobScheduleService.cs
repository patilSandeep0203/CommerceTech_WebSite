using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Schedules;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IJobScheduleService
    {
        int Insert(JobScheduleAddRequest model, int userId);
        Paged<JobSchedule> GetAll(int pageIndex, int pageSize);
        JobSchedule GetById(int id);
        List<JobSchedule> Current(int userId);
        List<JobSchedule> GetByOrgId(int userId, int orgId);
        void Update(JobScheduleUpdateRequest model, int userId);
        void Delete(int id);
   }
}

