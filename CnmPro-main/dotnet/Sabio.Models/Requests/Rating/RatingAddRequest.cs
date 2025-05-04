using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Rating
{
    public class RatingAddRequest
    {
        [Required]
        public int Rating { get; set; }
        [Required]
        public int CommentId { get; set; }
        [Required]
        public int EntityTypeId { get; set; }
        [Required]
        public int EntityId { get; set; }
     
        [Required]
        public bool IsDeleted { get; set; }
    }
}
