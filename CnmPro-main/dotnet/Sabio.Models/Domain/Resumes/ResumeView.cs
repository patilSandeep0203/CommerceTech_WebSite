using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Resumes
{
    public class ResumeView
    {
        public int Id  { get; set; }

        public int ViewerId { get; set; }

        public int ResumeId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
