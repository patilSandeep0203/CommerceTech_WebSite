using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Requests.Proposals
{
    public class ProposalAddRequest
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Bid { get; set; }
        [Required]
        [MaxLength(50)]
        public string Duration { get; set; }
        [Required]
        [MaxLength(4000)]
        public string Details { get; set; }
    }
}
