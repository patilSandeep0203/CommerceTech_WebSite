using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Comments
{
    public class CommentAddRequest 
    {
        [Required]
        [StringLength(3000)]
        public string Text { get; set; }
        [Range(int.MinValue, int.MaxValue)]
        public int? ParentId { get; set; }
        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int EntityTypeId { get; set; }
        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int EntityId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
