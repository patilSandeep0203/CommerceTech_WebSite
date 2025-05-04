using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Licenses
{
    public class LicenseAddRequest
    {
        public int LicenseStateId { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime DateExpires { get; set; }
    }
}
