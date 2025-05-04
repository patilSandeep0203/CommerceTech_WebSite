using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.User
{
    public class FacebookUpdateRequest : UserThirdPartyAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
