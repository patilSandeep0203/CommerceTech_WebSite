using Sabio.Models;
using Sabio.Models.Domain.Proposals;
using Sabio.Models.Requests.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Interfaces
{
    public interface IProposalService
    {
        int AddProposal(ProposalAddRequest model, int userId);
        Proposal GetProposalById(int id);
        Paged<Proposal> SelectProposalByCreatedBy(int CreatedBy, int pageIndex, int pageSize);
        Paged<Proposal> SelectAll(int pageIndex, int pageSize);
        void Update(ProposalUpdateRequest model, int userId);
        void DeleteProposal(int id);
    }
}
