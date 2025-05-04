using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Rating
{
    public class RatingUpdateRequest: RatingAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
