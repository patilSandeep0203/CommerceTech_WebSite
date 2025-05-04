using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Surveys
{
    public class Survey 
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string Description { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public LookUp Status { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public LookUp SurveyType { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int CreatedBy { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string CreatorFirstName { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string CreatorLastName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string CompanyLogo { get; set; }

        public DateTime DateModified { get; set; }
        
        public DateTime DateCreated { get; set; }
    }
}
