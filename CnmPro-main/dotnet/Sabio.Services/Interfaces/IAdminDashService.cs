using Sabio.Models;
using Sabio.Models.Domain.AdminDashboard;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IAdminDashService
    {
        List<AdminDashStats> GetAdminDashStats();
        List<AdminDashStats> GetAdminDashWeekStats();
        List<AdminDashStatsPerWeek> GetAdminDashUserWeekStats();
        List<AdminDashStatsPerWeek> GetAdminDashUserTwoWeekStats();
        List<AdminDashStatsPerWeek> GetAdminDashUserMonthStats();
        List<AdminDashStatsPerWeek> GetAdminDashUserYearStats();
        List<AdminDashOrgTableStats> GetAdminDashOrgTable();
    }
}