using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Timesheet;
using Sabio.Models.Requests.Timesheet;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class TimesheetService : ITimesheetService
    {
        IDataProvider _data = null;

        public TimesheetService(IDataProvider data)
        {
            _data = data;
        }

        public int Create(TimesheetAddRequest model, int userId)
        {
            int Id = 0;
            string procName = "[dbo].[Timesheet_Insert]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                AddCommonParams(model, paramCollection);
                paramCollection.AddWithValue("CreatedBy", userId);

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                paramCollection.Add(idOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;

                int.TryParse(oId.ToString(), out Id);
            });
            return Id;
        }

        public void Update(TimesheetUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Timesheet_Update]";

            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection paramCollection)
            {
                AddCommonParams(model, paramCollection);
                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@ModifiedBy", userId);

            });
        }

        public void Delete(int Id)
        {
            string procName = "[dbo].[Timesheet_Delete]";

            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", Id);
            });
        }

        public Paged<Timesheet> GetAllByUserPagination(int userId, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Timesheet_SelectAllByUser_Pagination]";
            Paged<Timesheet> pagedTimesheets = null;
            List<Timesheet> timesheets = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@userId", userId);
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Timesheet timesheet = MapSingleTimesheet(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (timesheets == null)
                {
                    timesheets = new List<Timesheet>();
                }
                timesheets.Add(timesheet);

            });
            if (timesheets != null)
            {
                pagedTimesheets = new Paged<Timesheet>(timesheets, pageIndex, pageSize, totalCount);
            }
            return pagedTimesheets;
        }

        public Paged<Timesheet> SearchByFilter(int userId, int organizationId, int dateRange, int pageIndex, int pageSize)
        {
            Paged<Timesheet> pagedList = null;
            List<Timesheet> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Timesheet_SearchFilter]";

        _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", userId);
                paramCollection.AddWithValue("@OrganizationId", organizationId);
                paramCollection.AddWithValue("@DateRange", dateRange);
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Timesheet timesheet = MapSingleTimesheet(reader, ref startingIndex);
                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Timesheet>();
                }
                list.Add(timesheet);
            });
            if (list != null)
            {
                pagedList = new Paged<Timesheet>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }


        public Paged<Timesheet> SearchByOrgCurrentUser(int userId, int organizationId, int pageIndex, int pageSize)
        {
            Paged<Timesheet> pagedList = null;
            List<Timesheet> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Timesheet_SelectAllOrg_User]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", userId);
                paramCollection.AddWithValue("@OrganizationId", organizationId);
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Timesheet timesheet = MapSingleTimesheet(reader, ref startingIndex);
                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Timesheet>();
                }
                list.Add(timesheet);
            });
            if (list != null)
            {
                pagedList = new Paged<Timesheet>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public int TotalHours(int userId)
        {
            string procName = "[dbo].[TimesheetTotalByUser]";
            int total = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", userId);
            }, delegate (IDataReader reader, short set)
            {
                total = reader.GetSafeInt32(0);
            });
            return total;
        }

        public List<UserJob> GetUserJobs(int userId)
        {
            List<UserJob> list = null;
            string procName = "[dbo.UserJobs_SelectById]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", userId);
            }, delegate (IDataReader reader, short set)
            {
                UserJob job = new UserJob();
                int startingIndex = 0;

                job.UserId = reader.GetSafeInt32(startingIndex++);
                job.JobId = reader.GetSafeInt32(startingIndex++);
                job.Title = reader.GetSafeString(startingIndex++);
                job.Organization = reader.GetSafeString(startingIndex++);
                job.OrganizationId = reader.GetSafeInt32(startingIndex++);

                if (list == null)
                {
                    list = new List<UserJob>();
                }
                list.Add(job);

            });
            return list;
        }

        public List<UserOrganization> UserOrganizations(int userId)
        {
            List<UserOrganization> list = null;
            string procName = "[dbo].[GetUserOrganizations]";

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", userId);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;

                UserOrganization organization = new UserOrganization();
                organization.Organization = reader.GetSafeString(startingIndex++);
                organization.OrganizationId = reader.GetSafeInt32(startingIndex++);


                if (list == null)
                {
                    list = new List<UserOrganization>();
                }

                list.Add(organization);
            });
            return list;
        }

        private static void AddCommonParams(TimesheetAddRequest model, SqlParameterCollection paramCollection)
        {
            paramCollection.AddWithValue("@Date", model.Date);
            paramCollection.AddWithValue("@TimeFrom", model.TimeFrom);
            paramCollection.AddWithValue("@TimeTo", model.TimeTo);
            paramCollection.AddWithValue("@Comment", model.Comment);
            paramCollection.AddWithValue("@OrgId", model.OrganizationId);
            paramCollection.AddWithValue("@JobId", model.JobId);
        }

        private static Timesheet MapSingleTimesheet(IDataReader reader, ref int startingIndex)
        {
            Timesheet timesheet = new Timesheet();

            timesheet.Id = reader.GetSafeInt32(startingIndex++);
            timesheet.Date = reader.GetSafeDateTime(startingIndex++);
            timesheet.TimeFrom = reader.GetSafeString(startingIndex++);
            timesheet.TimeTo = reader.GetSafeString(startingIndex++);
            timesheet.Comment = reader.GetSafeString(startingIndex++);
            timesheet.OrganizationId = reader.GetSafeInt32(startingIndex++);
            timesheet.Organization = reader.GetSafeString(startingIndex++);
            timesheet.LocationId = reader.GetSafeInt32(startingIndex++);
            timesheet.City = reader.GetSafeString(startingIndex++);
            timesheet.Title = reader.GetSafeString(startingIndex++);
            timesheet.JobId = reader.GetSafeInt32(startingIndex++);
            timesheet.UserId = reader.GetSafeInt32(startingIndex++);
            timesheet.TotalHrs = reader.GetSafeInt32(startingIndex++);

            return timesheet;
        }
    }
}