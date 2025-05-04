using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Organizations;
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
    public class OrganizationsFollowersServices : IOrganizationsFollowersServices
    {
        private IDataProvider _data = null;
        private IUserProfileMapper _userProfileMapper = null;

        public OrganizationsFollowersServices(IDataProvider data, IUserProfileMapper userProfileMapper)
        {
            _data = data;
            _userProfileMapper = userProfileMapper;
        }
        public void Add(int userId, int OrgId)
        {
            string procName = "[dbo].[OrganizationFollowers_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    OrgParams(userId, OrgId, collection);
                },
                returnParameters: null);
        }
        public Paged<UserProfileBase> GetByOrganizationId(int organizationId, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[OrganizationFollowers_SelectById]";
            Paged<UserProfileBase> pagedList = null;
            List<UserProfileBase> list = null;
            int totalCount = 0;
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@OrganizationId", organizationId);
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                UserProfileBase user = userSingleMapper(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<UserProfileBase>();
                }
                list.Add(user);
                }
                );
                if (list != null)
                {
                    pagedList = new Paged<UserProfileBase>(list, pageIndex, pageSize, totalCount);
                }
                return pagedList;
        }
        public Paged<OrganizationBase> GetOrgByUserId(int followerId, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[OrganizationFollowers_SelectByIdUser]";
            Paged<OrganizationBase> pagedList = null;
            List<OrganizationBase> list = null;
            int totalCount = 0;
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@FollowerId", followerId);
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
        {
            int startingIndex = 0;

            OrganizationBase user = singleMapperOrg(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<OrganizationBase>();
                }
                list.Add(user);
                });
                if (list != null)
                {
                    pagedList = new Paged<OrganizationBase>(list, pageIndex, pageSize, totalCount);
                }
                return pagedList;
        }
        public void DeleteOrgFolById(int organizationId, int followerId)
        {
            string procName = "[dbo].[OrganizationFollowers_Delete_ById]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@OrganizationId", organizationId);
                    col.AddWithValue("@FollowerId", followerId);
                }, returnParameters: null);
        }
        private UserProfileBase userSingleMapper(IDataReader reader, ref int startingIndex)
        {
            UserProfileBase orgFol = new UserProfileBase();
            orgFol = new UserProfileBase();
            orgFol = _userProfileMapper.Map(reader, ref startingIndex);

            return orgFol;
        }
        private static OrganizationBase singleMapperOrg(IDataReader reader, ref int startingIndex)
        {
            OrganizationBase aOrganization = new OrganizationBase();
            aOrganization = new OrganizationBase();
            aOrganization.Id = reader.GetSafeInt32(startingIndex++);
            aOrganization.Name = reader.GetSafeString(startingIndex++);
            aOrganization.Headline = reader.GetSafeString(startingIndex++);
            aOrganization.Description = reader.GetSafeString(startingIndex++);
            aOrganization.Logo = reader.GetSafeString(startingIndex++);
            aOrganization.Phone = reader.GetSafeString(startingIndex++);
            aOrganization.SiteUrl = reader.GetSafeString(startingIndex++);
            return aOrganization;
        }
        private static void OrgParams(int userId, int orgId,  SqlParameterCollection collection)
        {
            collection.AddWithValue("@OrganizationId", orgId);
            collection.AddWithValue("@FollowerId", userId);
        }
    }
}
