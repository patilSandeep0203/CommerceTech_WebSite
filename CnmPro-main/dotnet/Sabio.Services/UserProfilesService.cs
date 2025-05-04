using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Jobs;
using Sabio.Models.Domain.Newsletters;
using Sabio.Models.Domain.Timesheet;
using Sabio.Models.Domain.User;
using Sabio.Models.Requests.Licenses;
using Sabio.Models.Requests.UserProfiles;
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
    public class UserProfilesService : IUserProfilesService
    {
        public IDataProvider _data = null;
        ILookUpService _lookUp = null;
        IUserProfileMapper _profileMapper = null;

        public UserProfilesService(IDataProvider data, ILookUpService lookUp, IUserProfileMapper profileMapper)
        {
            _data = data;
            _lookUp = lookUp;
            _profileMapper = profileMapper;
        }

        public void DeleteById(int Id)
        {
            string procName = "[dbo].[UserProfiles_Delete_ById]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", Id);
                });
        }

        public UserProfile GetByCurrentId(int userId)
        {
            string procName = "[dbo].[UserProfiles_Select_ById]";
            int startingIndex = 0;

            UserProfile aUser = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@UserId", userId);

            }, delegate (IDataReader reader, short set)
            {
                aUser = MapAUser(reader, ref startingIndex);
            });

            return aUser;
        }

        public Paged<UserProfile> GetAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[UserProfiles_SelectAll]";

            Paged<UserProfile> pagedList = null;
            List<UserProfile> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (param) =>
            {
                param.AddWithValue("@PageIndex", pageIndex);
                param.AddWithValue("@PageSize", pageSize);
            }, (reader, recordSetIndex) =>
            {
                int startingIndex = 0;
                UserProfile userProfile = MapAUser(reader, ref startingIndex);
                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<UserProfile>();
                }

                list.Add(userProfile);
            });

            if (list != null)
            {
                pagedList = new Paged<UserProfile>(list, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }

        public Paged<UserProfile> Pagination(int pageIndex, int pageSize, int createdBy)
        {
            Paged<UserProfile> pagedList = null;
            List<UserProfile> list = null;
            int totalCount = 0;

            _data.ExecuteCmd("[dbo].[UserProfiles_Select_ByCreatedBy]", (param) =>
            {
                param.AddWithValue("@PageIndex", pageIndex);
                param.AddWithValue("@PageSize", pageSize);
                param.AddWithValue("@CreatedBy", createdBy);
            },
            (reader, recordSetIndex) =>
            {
                int startingIndex = 0;
                UserProfile userProfile = MapAUser(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<UserProfile>();
                }

                list.Add(userProfile);
            });
            if (list != null)
            {
                pagedList = new Paged<UserProfile>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public int AddV2(UserProfileAddRequestV2 model, int userId)
        {
            int Id = 0;
            DataTable professions = null;
            DataTable licenses = null;

            string procName = "[dbo].[UserProfiles_InsertV2]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    if (professions == null)
                    {
                        professions = MappingProfessionsV2(model.Profession);
                    }

                    if (licenses == null)
                    {
                        licenses = MappingLicensesV2(model.License, userId);
                    }

                    AddCommonParamsV2(model, col);
                    col.AddWithValue("@UserId", userId);
                    col.AddWithValue("@BatchProfessionTypes", professions);
                    col.AddWithValue("@BatchLicense", licenses);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;

                    int.TryParse(oId.ToString(), out Id);

                });

            return Id;
        }

        public void UpdateV2(UserProfileUpdateRequestV2 model, int userId)
        {
            DataTable professions = null;
            DataTable licenses = null;

            string procName = "[dbo].[UserProfiles_UpdateV2]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    if (professions == null)
                    {
                        professions = MappingProfessionsV2(model.Profession);
                    }

                    if (licenses == null)
                    {
                        licenses = MappingLicensesV2(model.License, userId);
                    }

                    AddCommonParamsV2(model, col);
                    col.AddWithValue("@UserId", userId);
                    col.AddWithValue("@BatchProfessionTypes", professions);
                    col.AddWithValue("@BatchLicense", licenses);
                    col.AddWithValue("@Id", model.Id);

                },
                returnParameters: null);
        }

        public UserDashboard GetDashboard(int userId)
        {
            UserDashboard dashboard = new UserDashboard();
            List<Newsletter> newsletterList = null;
            List<BaseTimesheet> timesheetList = null;
            List<OrganizationStats> orgStatsList = null;
            List<JobBase> jobList = null;

            _data.ExecuteCmd("[dbo].[userDashboard]", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@UserId", userId);
            },
              singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;

                switch (set)
                {
                    case 0:
                        dashboard.Blog = MapSingleBlog(reader, ref startingIndex);
                        break;

                    case 1:
                        Newsletter newsletter = MapNewsletter(reader, ref startingIndex);

                        if (newsletterList == null)
                        {
                            newsletterList = new List<Newsletter>();
                        }
                        newsletterList.Add(newsletter);
                        break;

                    case 2:
                        BaseTimesheet timesheet = MapBaseTimesheet(reader, ref startingIndex);

                        if (timesheetList == null)
                        {
                            timesheetList = new List<BaseTimesheet>();
                        }
                        timesheetList.Add(timesheet);
                        break;

                    case 3:
                        OrganizationStats orgStats = MapOrgStats(reader, ref startingIndex);

                        if (orgStatsList == null)
                        {
                            orgStatsList = new List<OrganizationStats>();
                        }
                        orgStatsList.Add(orgStats);
                        break;

                    case 4:
                        JobBase job = MapJobBase(reader, ref startingIndex);

                        if (jobList == null)
                        {
                            jobList = new List<JobBase>();
                        }
                        jobList.Add(job);
                        break;
                }
            });
            dashboard.Newsletters = newsletterList;
            dashboard.Timesheets = timesheetList;
            dashboard.OrgStats = orgStatsList;
            dashboard.Jobs = jobList;
            return dashboard;
        }
        private Blog MapSingleBlog(IDataReader reader, ref int startingIndex)
        {
            Blog blog = new Blog();
            blog.BlogType = new LookUp();


            blog.Id = reader.GetSafeInt32(startingIndex++);
            blog.Title = reader.GetSafeString(startingIndex++);
            blog.Subject = reader.GetSafeString(startingIndex++);
            blog.Content = reader.GetSafeString(startingIndex++);
            blog.IsPublished = reader.GetSafeBool(startingIndex++);
            blog.ImageUrl = reader.GetSafeString(startingIndex++);
            blog.DateCreated = reader.GetSafeDateTime(startingIndex++);
            blog.DateModified = reader.GetSafeDateTime(startingIndex++);
            blog.DatePublish = reader.GetSafeDateTime(startingIndex++);
            blog.BlogType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            blog.Author = _profileMapper.Map(reader, ref startingIndex);

            return blog;
        }

        private static JobBase MapJobBase(IDataReader reader, ref int startingIndex)
        {
            JobBase job = new JobBase();
            job.Id = reader.GetSafeInt32(startingIndex++);
            job.Title = reader.GetSafeString(startingIndex++);
            job.Description = reader.GetSafeString(startingIndex++);
            job.Requirements = reader.GetSafeString(startingIndex++);
            return job;
        }

        private static OrganizationStats MapOrgStats(IDataReader reader, ref int startingIndex)
        {
            OrganizationStats orgStats = new OrganizationStats();
            orgStats.Name = reader.GetSafeString(startingIndex++);
            orgStats.OrgId = reader.GetSafeInt32(startingIndex++);
            orgStats.Total = reader.GetSafeInt32(startingIndex++);
            return orgStats;
        }

        private static BaseTimesheet MapBaseTimesheet(IDataReader reader, ref int startingIndex)
        {
            BaseTimesheet timesheet = new BaseTimesheet();
            timesheet.Date = reader.GetSafeDateTime(startingIndex++);
            timesheet.TotalHrs = reader.GetSafeInt32(startingIndex++);
            timesheet.OrgId = reader.GetSafeInt32(startingIndex++);
            timesheet.Name = reader.GetSafeString(startingIndex++);
            timesheet.Logo = reader.GetSafeString(startingIndex++);
            return timesheet;
        }

        private static Newsletter MapNewsletter(IDataReader reader, ref int startingIndex)
        {
            Newsletter newsletter = new Newsletter();
            newsletter.Id = reader.GetSafeInt32(startingIndex++);
            newsletter.PublishDate = reader.GetSafeDateTime(startingIndex++);
            newsletter.Name = reader.GetSafeString(startingIndex++);
            newsletter.Description = reader.GetSafeString(startingIndex++);
            newsletter.Url = reader.GetSafeString(startingIndex++);
            return newsletter;
        }

        private static DataTable MappingProfessionsV2(List<string> Professions)
        {
            DataTable data = null;

            if (Professions != null)
            {
                data = new DataTable();
                data.Columns.Add("Name", typeof(String));

                foreach (string name in Professions)
                {
                    DataRow dr = data.NewRow();
                    int startingIndex = 0;
                    dr.SetField(startingIndex++, name);

                    data.Rows.Add(dr);
                }
            }
            return data;
        }

        private static DataTable MappingLicensesV2(List<LicenseAddRequest> Licenses, int userId)
        {
            DataTable data = null;

            if (Licenses != null)
            {
                data = new DataTable();
                data.Columns.Add("LicenseStateId", typeof(Int32));
                data.Columns.Add("LicenseTypeId", typeof(Int32));
                data.Columns.Add("LicenseNumber", typeof(String));
                data.Columns.Add("DateExpires", typeof(DateTime));
                data.Columns.Add("CreateBy", typeof(Int32));

                foreach (LicenseAddRequest license in Licenses)
                {
                    DataRow dr = data.NewRow();
                    int startingIndex = 0;
                    dr.SetField(startingIndex++, license.LicenseStateId);
                    dr.SetField(startingIndex++, license.LicenseTypeId);
                    dr.SetField(startingIndex++, license.LicenseNumber);
                    dr.SetField(startingIndex++, license.DateExpires);
                    dr.SetField(startingIndex++, userId);

                    data.Rows.Add(dr);
                }
            }
            return data;
        }

        private static void AddCommonParamsV2(UserProfileAddRequestV2 model, SqlParameterCollection col)
        {
            col.AddWithValue("@FirstName", model.FirstName);
            col.AddWithValue("@LastName", model.LastName);
            col.AddWithValue("@Mi", model.Mi);
            col.AddWithValue("@LocationTypeId", model.LocationTypeId);
            col.AddWithValue("@LineOne", model.LineOne);
            col.AddWithValue("@LineTwo", model.LineTwo);
            col.AddWithValue("@City", model.City);
            col.AddWithValue("@Zip", model.Zip);
            col.AddWithValue("@StateId", model.StateId);
            col.AddWithValue("@Latitude", model.Latitude);
            col.AddWithValue("@Longitude", model.Longitude);
            col.AddWithValue("@AvatarUrl", model.AvatarUrl);
            col.AddWithValue("@DOB", model.DOB);
            col.AddWithValue("@Email", model.Email);
            col.AddWithValue("@Phone", model.Phone);
            col.AddWithValue("@YearsOfExperience", model.YearsOfExperience);
            col.AddWithValue("@DesiredHourlyRate", model.DesiredHourlyRate);
            col.AddWithValue("@IsActive", model.IsActive);
        }

        private static UserProfile MapAUser(IDataReader reader, ref int startingIndex)
        {
            UserProfile aUser = new UserProfile();

            aUser.Id = reader.GetSafeInt32(startingIndex++);
            aUser.UserId = reader.GetSafeInt32(startingIndex++);
            aUser.FirstName = reader.GetSafeString(startingIndex++);
            aUser.LastName = reader.GetSafeString(startingIndex++);
            aUser.Mi = reader.GetSafeString(startingIndex++);
            aUser.AvatarUrl = reader.GetSafeString(startingIndex++);
            aUser.DOB = reader.GetSafeDateTime(startingIndex++);
            aUser.Email = reader.GetSafeString(startingIndex++);
            aUser.Phone = reader.GetSafeString(startingIndex++);
            aUser.YearsOfExperience = reader.GetSafeString(startingIndex++);
            aUser.DesiredHourlyRate = reader.GetSafeString(startingIndex++);
            aUser.IsActive = reader.GetSafeBool(startingIndex++);
            aUser.DateCreated = reader.GetSafeDateTime(startingIndex++);
            aUser.DateModified = reader.GetSafeDateTime(startingIndex++);
            aUser.Location = new Location();
            aUser.Location.Id = reader.GetSafeInt32(startingIndex++);
            aUser.Location.LocationType = new LookUp();
            aUser.Location.LocationType.Id = reader.GetSafeInt32(startingIndex++);
            aUser.Location.LocationType.Name = reader.GetSafeString(startingIndex++);
            aUser.Location.LineOne = reader.GetSafeString(startingIndex++);
            aUser.Location.LineTwo = reader.GetSafeString(startingIndex++);
            aUser.Location.City = reader.GetSafeString(startingIndex++);
            aUser.Location.Zip = reader.GetSafeString(startingIndex++);
            aUser.Location.State = new State();
            aUser.Location.State.Id = reader.GetSafeInt32(startingIndex++);
            aUser.Location.State.Code = reader.GetSafeString(startingIndex++);
            aUser.Location.State.Name = reader.GetSafeString(startingIndex++);
            aUser.Location.Latitude = reader.GetSafeDouble(startingIndex++);
            aUser.Location.Longitude = reader.GetSafeDouble(startingIndex++);
            aUser.License = reader.DeserializeObject<List<License>>(startingIndex++);
            aUser.Profession = reader.DeserializeObject<List<UserProfessionTypes>>(startingIndex++);

            return aUser;
        }
    }
}
