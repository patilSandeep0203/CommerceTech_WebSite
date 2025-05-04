using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Jobs;
using Sabio.Models.Domain.Organizations;
using Sabio.Models.Domain.Proposals;
using Sabio.Models.Requests.Proposals;
using Sabio.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sabio.Services
{
    public class ProposalService : IProposalService
    {
        IDataProvider _data = null;
        IUserProfileMapper _profileMapper = null;
        ILookUpService _lookUp = null;
        public ProposalService(IDataProvider data, IUserProfileMapper profileMapper, ILookUpService lookUp)
        {
            _data = data;
            _profileMapper = profileMapper;
            _lookUp = lookUp;
        }

        public Proposal GetProposalById(int id)
        {
            Proposal proposal = null;
            string procName = "[dbo].[Proposals_Select_ById]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                proposal = MapSingleProposal(reader, ref startingIndex);
            });

            return proposal;
        }

        public Paged<Proposal> SelectProposalByCreatedBy(int createdBy, int pageIndex, int pageSize)

        {
            Paged<Proposal> pagedList = null;
            List<Proposal> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Proposals_Select_ByCreatedBy]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@pageIndex", pageIndex);
                parameterCollection.AddWithValue("@pageSize", pageSize);
                parameterCollection.AddWithValue("@CreatedBy", createdBy);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Proposal proposal = MapSingleProposal(reader, ref startingIndex);
                totalCount = reader.GetSafeInt32(startingIndex++);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Proposal>();
                }

                list.Add(proposal);
            });
            if (list != null)
            {
                pagedList = new Paged<Proposal>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        public Paged<Proposal> SelectAll(int pageIndex, int pageSize)

        {
            Paged<Proposal> pagedList = null;
            List<Proposal> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Proposals_SelectAll]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Proposal proposal = MapSingleProposal(reader, ref startingIndex);
                totalCount = reader.GetSafeInt32(startingIndex++);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Proposal>();
                }

                list.Add(proposal);
            });
            if (list != null)
            {
                pagedList = new Paged<Proposal>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        public int AddProposal(ProposalAddRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[Proposals_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@CreatedBy", userId);
                    col.AddWithValue("@ModifiedBy", userId);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                });

            return id;
        }

        public void Update(ProposalUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Proposals_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", model.Id);
                    col.AddWithValue("@ModifiedBy", userId);
                    col.AddWithValue("@ProposalStatusId", model.ProposalId);
                    AddCommonParams(model, col);
                }, returnParameters: null);
        }

        public void DeleteProposal(int id)
        {
            string procName = "[dbo].[Proposals_Delete]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@id", id);
                }, returnParameters: null);
        }

        private Proposal MapSingleProposal(IDataReader reader, ref int startingIndex)
        {
            Proposal proposal = new Proposal();
            proposal.Id = reader.GetSafeInt32(startingIndex++);
            proposal.Job = new Job();
            proposal.Job.Id = reader.GetSafeInt32(startingIndex++);

            proposal.Job.JobType = new LookUp();
            proposal.Job.JobType.Name = reader.GetString(startingIndex++);

            proposal.Job.Location = new Location();
            proposal.Job.Location.LocationType = new LookUp();
            proposal.Job.Location.LocationType.Name = reader.GetSafeString(startingIndex++);
            proposal.Job.Location.LineOne = reader.GetSafeString(startingIndex++);
            proposal.Job.Location.LineTwo = reader.GetSafeString(startingIndex++);
            proposal.Job.Location.City = reader.GetSafeString(startingIndex++);
            proposal.Job.Location.Zip = reader.GetSafeString(startingIndex++);

            proposal.Job.Location.State = new State();
            proposal.Job.Location.State.Name = reader.GetSafeString(startingIndex++);
            proposal.Job.Location.State.Code = reader.GetSafeString(startingIndex++);
            proposal.Job.Location.Latitude = reader.GetSafeDouble(startingIndex++);
            proposal.Job.Location.Longitude = reader.GetSafeDouble(startingIndex++);

            proposal.Job.Organization = new Organization();
            proposal.Job.Organization.OrganizationType = new LookUp();
            proposal.Job.Organization.OrganizationType.Name = reader.GetSafeString(startingIndex++);

            proposal.Job.Organization.Name = reader.GetSafeString(startingIndex++);
            proposal.Job.Organization.Headline = reader.GetSafeString(startingIndex++);
            proposal.Job.Organization.Description = reader.GetSafeString(startingIndex++);
            proposal.Job.Organization.Logo = reader.GetSafeString(startingIndex++);
            proposal.Job.Organization.Phone = reader.GetSafeString(startingIndex++);
            proposal.Job.Organization.SiteUrl = reader.GetSafeString(startingIndex++);

            proposal.Job.Title = reader.GetSafeString(startingIndex++);
            proposal.Job.Description = reader.GetSafeString(startingIndex++);
            proposal.Job.Requirements = reader.GetSafeString(startingIndex++);
            proposal.Job.IsActive = reader.GetSafeBool(startingIndex++);
            proposal.Job.ContactName = reader.GetSafeString(startingIndex++);
            proposal.Job.ContactPhone = reader.GetSafeString(startingIndex++);
            proposal.Job.ContactEmail = reader.GetSafeString(startingIndex++);

            proposal.Bid = reader.GetSafeString(startingIndex++);
            proposal.Duration = reader.GetSafeString(startingIndex++);
            proposal.Details = reader.GetSafeString(startingIndex++);

            proposal.ProposalStatus = new LookUp();
            proposal.ProposalStatus.Name = reader.GetSafeString(startingIndex++);
            proposal.CreatedBy = reader.GetSafeInt32(startingIndex++);
            proposal.ModifiedBy = reader.GetSafeInt32(startingIndex++);

            return proposal;
        }
        private static void AddCommonParams(ProposalAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@JobId", model.JobId);
            col.AddWithValue("@Bid", model.Bid);
            col.AddWithValue("@Duration", model.Duration);
            col.AddWithValue("@Details", model.Details);
        }
    }
}
