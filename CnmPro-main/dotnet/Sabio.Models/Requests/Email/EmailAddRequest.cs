using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Email
{
    public class EmailAddRequest
    {
        
        public string Subject { get; set; }
        public string Body { get; set; }
        public string PlainText { get; set; }
        public string Email { get; set; }
    }
}
