using Sabio.Models.Domain.Jobs;
using System;

namespace Sabio.Models.Domain.Proposals
{
    public class Proposal
    {
        public int Id { get; set; }
        public Job Job { get; set; }
        public string Bid { get; set; }
        public string Duration { get; set; }
        public string Details { get; set; }
        public LookUp ProposalStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
