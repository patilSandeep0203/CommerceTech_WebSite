using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Timesheet;
using Sabio.Models.Requests.Timesheet;
using System;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface ITimesheetService
    {
        public int Create(TimesheetAddRequest model, int userId);
        public void Update(TimesheetUpdateRequest model, int userId);
        public void Delete(int Id);
        public Paged<Timesheet> GetAllByUserPagination(int userId, int pageIndex, int pageSize);
        public Paged<Timesheet> SearchByFilter(int userId, int organizationId, int dateRange, int pageIndex, int pageSize);
        public Paged<Timesheet> SearchByOrgCurrentUser(int userId, int organizationId, int pageIndex, int pageSize);
        public int TotalHours(int userId);
        public List<UserJob> GetUserJobs(int userId);
        public List<UserOrganization> UserOrganizations(int userId);

    }
}