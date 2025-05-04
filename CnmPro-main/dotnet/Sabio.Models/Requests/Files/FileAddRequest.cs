using Sabio.Models.Domain.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Files
{
    public class FileAddRequest
    {
    
        public string Url { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
    }
}
