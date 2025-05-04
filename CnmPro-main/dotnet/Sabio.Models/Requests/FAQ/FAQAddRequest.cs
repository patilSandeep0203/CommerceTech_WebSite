using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.FAQ
{
    public class FAQAddRequest
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

   
        public int CategoryId { get; set; }

    
        public int SortOrder { get; set; }


    }
}
