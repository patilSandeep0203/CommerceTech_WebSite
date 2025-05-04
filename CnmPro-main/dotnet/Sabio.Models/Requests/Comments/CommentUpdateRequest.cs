using System.ComponentModel.DataAnnotations;

namespace Sabio.Models.Requests.Comments
{
    public class CommentUpdateRequest : CommentAddRequest, IModelIdentifier
    {
        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Id { get; set; }
        [Range(int.MinValue, int.MaxValue)]
        public int UserId { get; set; }

    }
}
