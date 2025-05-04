using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain.AdminDashboard;
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
    public class AdminDashService : IAdminDashService
    {
        public IDataProvider _data = null;

        public AdminDashService(IDataProvider data)
        {
            _data = data;
        }

        public List<AdminDashStats> GetAdminDashStats()
        {
            string procName = "[dbo].[AdminDashboard_Select_Info]";
            List<AdminDashStats> countList = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
            delegate (IDataReader reader, short set)
            {
                AdminDashStats information = new AdminDashStats();
                MappingCounts(reader, information);
                if (countList == null)
                {
                    countList = new List<AdminDashStats>();
                }
                countList.Add(information);
            });
            return countList;
        }

        public List<AdminDashStats> GetAdminDashWeekStats() 
        { 
            string procName = "[dbo].[AdminDashboard_Select_InfoWeekly]";
            List<AdminDashStats> weekList = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
            delegate (IDataReader reader, short set)
            {
                AdminDashStats information = new AdminDashStats();
                MappingGrowthRatio(reader, information);
                if (weekList == null)
                {
                    weekList = new List<AdminDashStats>();
                }
                weekList.Add(information);
            });
            return weekList;
        }

        public List<AdminDashStatsPerWeek> GetAdminDashUserWeekStats()
        {
            string procName = "[dbo].[AdminDashboard_Select_UserWeek]";
            List<AdminDashStatsPerWeek> listOfUsers = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
                delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        AdminDashStatsPerWeek information = new AdminDashStatsPerWeek();
                        MappingChartStats(reader, information);

                        if (listOfUsers == null)
                        {
                            listOfUsers = new List<AdminDashStatsPerWeek>();
                        }
                        listOfUsers.Add(information);
                    }
                });
            return listOfUsers;
        }

        public List<AdminDashStatsPerWeek> GetAdminDashUserTwoWeekStats()
        {
            string procName = "[dbo].[AdminDashboard_Select_UserWeek]";
            List<AdminDashStatsPerWeek> listOfUsers = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
                delegate (IDataReader reader, short set)
                {
                    if (set == 1)
                    {
                        AdminDashStatsPerWeek information = new AdminDashStatsPerWeek();
                        MappingChartStats(reader, information);

                        if (listOfUsers == null)
                        {
                            listOfUsers = new List<AdminDashStatsPerWeek>();
                        }
                        listOfUsers.Add(information);
                    }
                });
            return listOfUsers;
        }

        public List<AdminDashStatsPerWeek> GetAdminDashUserMonthStats()
        {
            string procName = "[dbo].[AdminDashboard_Select_UserMonth]";
            List<AdminDashStatsPerWeek> listOfUsers = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
                delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        AdminDashStatsPerWeek information = new AdminDashStatsPerWeek();
                        MappingChartStats(reader, information);

                        if (listOfUsers == null)
                        {
                            listOfUsers = new List<AdminDashStatsPerWeek>();
                        }
                        listOfUsers.Add(information);
                    }
                });
            return listOfUsers;
        }

        public List<AdminDashStatsPerWeek> GetAdminDashUserYearStats()
        {
            string procName = "[dbo].[AdminDashboard_Select_UserMonth]";
            List<AdminDashStatsPerWeek> listOfUsers = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
                delegate (IDataReader reader, short set)
                {
                    if (set == 1)
                    {
                        AdminDashStatsPerWeek information = new AdminDashStatsPerWeek();
                        MappingChartStats(reader, information);

                        if (listOfUsers == null)
                        {
                            listOfUsers = new List<AdminDashStatsPerWeek>();
                        }
                        listOfUsers.Add(information);
                    }
                });
            return listOfUsers;
        }

        public List<AdminDashOrgTableStats> GetAdminDashOrgTable()
        {
            string procName = "[dbo].[AdminDashboard_Select_Organizations]";
            List<AdminDashOrgTableStats> listOfOrganizations = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
                delegate (IDataReader reader, short set)
                {
                    if (set == 0)
                    {
                        AdminDashOrgTableStats information = new AdminDashOrgTableStats();
                        MappingOrgData(reader, information);

                        if (listOfOrganizations == null)
                        {
                            listOfOrganizations = new List<AdminDashOrgTableStats>();
                        }
                        listOfOrganizations.Add(information);
                    }
                });
            return listOfOrganizations;
        }

        #region MappingMethods
        private static void MappingCounts(IDataReader reader, AdminDashStats information)
        {
            int startingIndex = 0;
            information.TotalOrg = reader.GetSafeInt32(startingIndex++);
            information.TotalUsers = reader.GetSafeInt32(startingIndex++);
            information.ActiveUsers = reader.GetSafeInt32(startingIndex++);
        }
        private static void MappingGrowthRatio(IDataReader reader, AdminDashStats information)
        {
            int startIndex = 0;
            information.OrgGrowth = reader.GetSafeDecimal(startIndex++);
            information.UsersGrowth = reader.GetSafeDecimal(startIndex++);
            information.ActiveUsersGrowth = reader.GetSafeDecimal(startIndex++);
        }
        private static void MappingChartStats(IDataReader reader, AdminDashStatsPerWeek information)
        {
            int starIndex = 0;
            information.CountUsers = reader.GetSafeInt32(starIndex++);
            information.DateOfTheDay = reader.GetSafeString(starIndex++);
        }
        private static void MappingOrgData(IDataReader reader, AdminDashOrgTableStats information)
        {
            int starIndex = 0;
            information.Id = reader.GetSafeInt32(starIndex++);
            information.Name = reader.GetSafeString(starIndex++);
            information.Headline = reader.GetSafeString(starIndex++);
            information.Logo = reader.GetSafeString(starIndex++);
            information.SiteUrl = reader.GetSafeString(starIndex++);
            information.City = reader.GetSafeString(starIndex++);
            information.LineOne = reader.GetSafeString(starIndex++);
            information.LineTwo = reader.GetSafeString(starIndex++);
            information.StateCode = reader.GetSafeString(starIndex++);
            information.Zip = reader.GetSafeString(starIndex++);
            information.DateCreated = reader.GetSafeDateTime(starIndex++);
        } 
        #endregion
    }
}

