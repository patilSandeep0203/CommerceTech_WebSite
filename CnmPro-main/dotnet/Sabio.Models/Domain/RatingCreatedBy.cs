using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class RatingCreatedBy
    {     
        public int Id { get; set; }
        public int Rating { get; set; }
        public int EntityId { get; set; }
        public int EntityTypeId { get; set; }
    }
}

