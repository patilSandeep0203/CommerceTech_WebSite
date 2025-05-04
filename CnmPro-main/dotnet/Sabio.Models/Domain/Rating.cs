using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public Byte RatingNumber { get; set; }
        public LookUp Comment { get; set; }  
        public LookUp EntityType { get; set; }   
        public int EntityId { get; set; }
        public DateTime DateCreated { get; set; }   
        public DateTime DateModified { get; set; }
        public int CreatedById { get; set; }  
        public bool IsDeleted { get; set; }
    }
}
