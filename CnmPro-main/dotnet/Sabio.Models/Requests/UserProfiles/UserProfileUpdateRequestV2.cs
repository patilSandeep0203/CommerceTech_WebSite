﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.UserProfiles
{
    public class UserProfileUpdateRequestV2: UserProfileAddRequestV2, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
