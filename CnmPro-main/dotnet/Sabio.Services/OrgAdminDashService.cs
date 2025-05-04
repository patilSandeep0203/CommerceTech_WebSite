using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain.OrgAdminDashboard;
using Sabio.Models.Domain;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Models.Domain.Jobs;
using Sabio.Models.Domain.Organizations;

namespace Sabio.Services
{
    public class OrgAdminDashService : IOrgAdminDashService
    {
        public IDataProvider _data = null;
        IUserProfileMapper _profileMapper = null;
        ILookUpService _lookUp = null;

        public OrgAdminDashService(IDataProvider data, IUserProfileMapper profileMapper, ILookUpService lookUp)
        {
            _data = data;
            _profileMapper = profileMapper;
            _lookUp = lookUp;
        }

        public JobTypeStats GetJobTypeStatsByOrg(int id)
        {
            string procName = "[dbo].[OrgAdminDashboard_Select_JobsByJobType]";
            JobTypeStats stats = null;
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paraCol)
            {
                paraCol.AddWithValue("@Id", id);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                stats = MapJobTypeStats(reader);
            });
            return stats;
        }

        public JobsByActiveStatus GetJobsByActiveStatus(int id)
        {
            string procName = "[dbo].[OrgAdminDashboard_Select_JobsByActiveStatus]";
            JobsByActiveStatus jobs = null;
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paraCol)
            {
                paraCol.AddWithValue("@Id", id);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                jobs = MapJobsByActiveStatus(reader);
            });
            return jobs;
        }


        public Paged<Job> GetJobsByOrganizationPaginated(int id, int pageIndex, int pageSize)
        {
            Paged<Job> pagedResult = null;
            List<Job> jobs = null;
            int totalCount = 0;
            string procName = "[dbo].[Jobs_Select_ByOrganization_Paginated]";
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paraCol)
            {
                paraCol.AddWithValue("@OrganizationId", id);
                paraCol.AddWithValue("@PageIndex", pageIndex);
                paraCol.AddWithValue("@PageSize", pageSize);

            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Job job = MapJobsByOrg(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (jobs == null)
                {
                    jobs = new List<Job>();
                }

                jobs.Add(job);
            });

            if (jobs != null)
            {
                pagedResult = new Paged<Job>(jobs, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }

        public Paged<ProposalsByOrg> GetProposalsByOrg(int id, int pageIndex, int pageSize)
        {
            Paged<ProposalsByOrg> pagedResult = null;
            List<ProposalsByOrg> proposals = null;
            int totalCount = 0;
            string procName = "[dbo].[OrgAdminDashboard_GetProsposalsByOrg]";
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paraCol)
            {
                paraCol.AddWithValue("@OrganizationId", id);
                paraCol.AddWithValue("@PageIndex", pageIndex);
                paraCol.AddWithValue("@PageSize", pageSize);

            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                ProposalsByOrg proposal = MapProposalsByOrg(reader, ref totalCount);

                if (proposals == null)
                {
                    proposals = new List<ProposalsByOrg>();
                }

                proposals.Add(proposal);
            });

            if (proposals != null)
            {
                pagedResult = new Paged<ProposalsByOrg>(proposals, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }

        private static ProposalsByOrg MapProposalsByOrg(IDataReader reader, ref int totalCount)
        {
            int startingIndex = 0;
            ProposalsByOrg proposal = new ProposalsByOrg();
            proposal.JobType = new LookUp();
            proposal.Id = reader.GetSafeInt32(startingIndex++);
            proposal.JobId = reader.GetSafeInt32(startingIndex++);
            proposal.JobType.Name = reader.GetSafeString(startingIndex++);
            proposal.Occupation = reader.GetSafeString(startingIndex++);
            proposal.JobTitle = reader.GetSafeString(startingIndex++);
            proposal.JobDescription = reader.GetSafeString(startingIndex++);
            proposal.Bid = reader.GetSafeString(startingIndex++);
            proposal.Duration = reader.GetSafeString(startingIndex++);
            proposal.Details = reader.GetSafeString(startingIndex++);
            proposal.ProposalStatus = reader.GetSafeString(startingIndex++);
            proposal.DateCreated = reader.GetSafeDateTime(startingIndex++);
            proposal.CreatedBy = reader.GetSafeInt32(startingIndex++);
            proposal.Email = reader.GetSafeString(startingIndex++);
            proposal.ModifiedBy = reader.GetSafeInt32(startingIndex++);
            if (totalCount == 0)
            {
                totalCount = reader.GetSafeInt32(startingIndex++);
            }
            return proposal;
        }


        public List<ProposalCountByDay> GetLastSevenDaysOfProposals(int id, int timezoneOffset)
        {
            List<ProposalCountByDay> counts = null;
            string procName = "[dbo].[OrgAdminDashBoard_GetProsposalStats]";
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paraCol)
            {
                paraCol.AddWithValue("@OrganizationId", id);
                paraCol.AddWithValue("@TimeZoneOffset", timezoneOffset);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                ProposalCountByDay count = MapProposals(reader);

                if (counts == null)
                {
                    counts = new List<ProposalCountByDay>();
                }
                counts.Add(count);
            });
            return counts;
        }

        public List<ProposalCountByDay> GetLast15DaysOfProposals(int id, int timezoneOffset)
        {
            List<ProposalCountByDay> counts = null;
            string procName = "[dbo].[OrgAdminDashBoard_GetProsposalStats15]";
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paraCol)
            {
                paraCol.AddWithValue("@OrganizationId", id);
                paraCol.AddWithValue("@TimeZoneOffset", timezoneOffset);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                ProposalCountByDay count = MapProposals(reader);

                if (counts == null)
                {
                    counts = new List<ProposalCountByDay>();
                }
                counts.Add(count);
            });
            return counts;
        }

        public List<ProposalCountByDay> GetLast30DaysOfProposals(int id, int timezoneOffset)
        {
            List<ProposalCountByDay> counts = null;
            string procName = "[dbo].[OrgAdminDashBoard_GetProsposalStats30]";
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paraCol)
            {
                paraCol.AddWithValue("@OrganizationId", id);
                paraCol.AddWithValue("@TimeZoneOffset", timezoneOffset);
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                ProposalCountByDay count = MapProposals(reader);

                if (counts == null)
                {
                    counts = new List<ProposalCountByDay>();
                }
                counts.Add(count);
            });
            return counts;
        }

        private static ProposalCountByDay MapProposals(IDataReader reader)
        {
            int startingIndex = 0;
            ProposalCountByDay count = new ProposalCountByDay();
            count.DateCreated = reader.GetSafeDateTime(startingIndex++);
            count.Count = reader.GetSafeInt32(startingIndex++);
            return count;
        }

        private static JobTypeStats MapJobTypeStats(IDataReader reader)
        {
            int startingIndex = 0;
            JobTypeStats stats = new JobTypeStats();
            stats.PartTimeCount = reader.GetSafeInt32(startingIndex++);
            stats.FullTimeCount = reader.GetSafeInt32(startingIndex++);
            stats.ContractCount = reader.GetSafeInt32(startingIndex++);
            return stats;
        }


        private Job MapJobsByOrg(IDataReader reader, ref int startingIndex)
        {
            Job job = new Job();
            job.Id = reader.GetSafeInt32(startingIndex++);
            job.Title = reader.GetSafeString(startingIndex++);
            job.Description = reader.GetSafeString(startingIndex++);
            job.Requirements = reader.GetSafeString(startingIndex++);
            job.IsActive = reader.GetSafeBool(startingIndex++);
            job.Organization = new Organization();
            job.Organization.Id = reader.GetSafeInt32(startingIndex++);
            job.Organization.OrganizationType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            job.Organization.Name = reader.GetSafeString(startingIndex++);
            job.Organization.Headline = reader.GetSafeString(startingIndex++);
            job.Organization.Description = reader.GetSafeString(startingIndex++);
            job.Organization.Logo = reader.GetSafeString(startingIndex++);
            job.Organization.Phone = reader.GetSafeString(startingIndex++);
            job.Organization.SiteUrl = reader.GetSafeString(startingIndex++);
            job.ContactName = reader.GetSafeString(startingIndex++);
            job.ContactPhone = reader.GetSafeString(startingIndex++);
            job.ContactEmail = reader.GetSafeString(startingIndex++);
            job.CreatedBy = _profileMapper.Map(reader, ref startingIndex);
            job.JobType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            job.Location = new Location();
            job.Location.LocationType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            job.Location.Id = reader.GetSafeInt32(startingIndex++);
            job.Location.LineOne = reader.GetSafeString(startingIndex++);
            job.Location.LineTwo = reader.GetSafeString(startingIndex++);
            job.Location.City = reader.GetSafeString(startingIndex++);
            job.Location.State = new State();
            job.Location.State.Name = reader.GetSafeString(startingIndex++);
            job.Location.Zip = reader.GetSafeString(startingIndex++);
            job.Location.State.Id = reader.GetSafeInt32(startingIndex++);
            job.Location.Latitude = reader.GetSafeDouble(startingIndex++);
            job.Location.Longitude = reader.GetSafeDouble(startingIndex++);
            job.DateCreated = reader.GetSafeDateTime(startingIndex++);
            return job;
        }




        private static JobsByActiveStatus MapJobsByActiveStatus(IDataReader reader)
        {
            int startingIndex = 0;
            JobsByActiveStatus jobs = new JobsByActiveStatus();
            jobs.ActiveJobCount = reader.GetSafeInt32(startingIndex++);
            jobs.InactiveCount = reader.GetSafeInt32(startingIndex++);
            return jobs;
        }
    }
}
