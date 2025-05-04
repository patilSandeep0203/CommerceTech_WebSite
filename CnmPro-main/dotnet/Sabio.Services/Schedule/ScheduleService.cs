using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain.Organizations;
using Sabio.Models.Domain.Schedules;
using Sabio.Models.Requests.Schedules;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Schedules
{
    public class ScheduleService : IScheduleService
    {
        IDataProvider _data = null;
        IUserProfileMapper _profileMapper = null;

        public ScheduleService(IDataProvider data, IUserProfileMapper profileMapper)
        {
            _data = data;
            _profileMapper = profileMapper;
        }

        public void DeleteById(int id)
        {
            string procName = "[dbo].[Schedules_Delete_ById]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);

                },
                returnParameters: null
                ) ;
        }

        public Paged<Schedule> GetByCreatedBy(int pageIndex, int pageSize, int userId)
        {
            Paged<Schedule> schedule = null;
            List<Schedule> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Schedules_Select_ByCreatedBy]";
            _data.ExecuteCmd(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageIndex", pageIndex);
                    paramCollection.AddWithValue("@PageSize", pageSize);
                    paramCollection.AddWithValue("@UserId", userId);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;

                    Schedule schedule = MapSingleSchedule(reader, ref index);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);
                    }

                    if (list == null)
                    {
                        list = new List<Schedule>();
                    }
                    list.Add(schedule);
                }
                );
            if (list != null)
            {
                schedule = new Paged<Schedule>(list, pageIndex, pageSize, totalCount);
            }
            return schedule;
        }

        public void Update(ScheduleUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Schedules_Update]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@OrgId", model.OrgId);
                    paramCollection.AddWithValue("@CreatedBy", model.CreatedBy);
                    paramCollection.AddWithValue("@ModifiedBy", userId);
                },
                null);
        }

        public int Insert(ScheduleAddRequest model, int userId)
        {
            int scheduleId = 0;
            string procName = "[dbo].[Schedules_Insert]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@OrgId", model.OrgId);
                    paramCollection.AddWithValue("@CreatedBy", userId);
                    paramCollection.AddWithValue("@ModifiedBy", userId);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = ParameterDirection.Output;
                    paramCollection.Add(idParameter);
                },
                returnParameters: delegate (SqlParameterCollection returnParam)
                {
                    Int32.TryParse(returnParam["@Id"].Value.ToString(), out scheduleId);
                }
                );

            return scheduleId;
        }

        public Schedule GetById(int id)
        {
            Schedule schedule = null;
            string procName = "[dbo].[Schedules_Select_ById]";

            _data.ExecuteCmd(
                procName,
                inputParamMapper: delegate (SqlParameterCollection paramcol)
                {
                    paramcol.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;

                    schedule = MapSingleSchedule(reader, ref index);

                }
                );
            return schedule;
        }

        public Paged<Schedule> GetAll(int pageIndex, int pageSize)
        {
            Paged<Schedule> pagedList = null;
            List<Schedule> list = null;

            int totalCount = 0;
            string procName = "[dbo].[Schedules_SelectAll]";

            _data.ExecuteCmd(
                procName,
                inputParamMapper: delegate (SqlParameterCollection paramcol)
                {
                    paramcol.AddWithValue("@PageIndex", pageIndex);
                    paramcol.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;

                    Schedule schedule = MapSingleSchedule(reader, ref index);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);
                    }

                    if (list == null)
                    {
                        list = new List<Schedule>();
                    }

                    list.Add(schedule);
                }
                );

            if (list != null)
            {
                pagedList = new Paged<Schedule>(list, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }

        private Schedule MapSingleSchedule(IDataReader reader, ref int index)
        {
            Schedule schedule = new Schedule();

            schedule.Id = reader.GetSafeInt32(index++);
            schedule.Organization = reader.GetSafeString(index++);
            schedule.CreatedBy = _profileMapper.Map(reader, ref index);
            schedule.ModifiedBy = _profileMapper.Map(reader, ref index);
            schedule.DateCreated = reader.GetSafeDateTime(index++);
            schedule.DateModified = reader.GetSafeDateTime(index++);

            return schedule;
        }

    }
}
