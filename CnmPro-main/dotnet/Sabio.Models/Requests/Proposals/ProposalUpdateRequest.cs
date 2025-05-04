using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Requests.Proposals
{
    public class ProposalUpdateRequest : ProposalAddRequest, IModelIdentifier
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProposalId { get; set; }
    }
}
