﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Surveys
{
    public class SurveyUpdateRequest : SurveyAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
