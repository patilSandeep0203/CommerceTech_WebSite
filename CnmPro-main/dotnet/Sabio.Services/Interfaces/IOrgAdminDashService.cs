using Sabio.Models;
using Sabio.Models.Domain.Jobs;
using Sabio.Models.Domain.OrgAdminDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface IOrgAdminDashService
    {
        JobTypeStats GetJobTypeStatsByOrg(int id);

        JobsByActiveStatus GetJobsByActiveStatus(int id);
        Paged<Job> GetJobsByOrganizationPaginated(int id, int pageIndex, int pageSize);
        List<ProposalCountByDay> GetLastSevenDaysOfProposals(int id, int timezoneOffset);
        List<ProposalCountByDay> GetLast15DaysOfProposals(int id, int timezoneOffset);
        List<ProposalCountByDay> GetLast30DaysOfProposals(int id, int timezoneOffset);
        Paged<ProposalsByOrg> GetProposalsByOrg(int id, int pageIndex, int pageSize);
    }
}
