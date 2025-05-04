﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.SurveyQuestions
{
    public class SurveyQuestion
    {
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public int UserId { get; set; }

        public string Question { get; set; }

        public string HelpText { get; set; }

        public bool IsRequired { get; set; }

        public bool IsMultipleAllowed { get; set; }

        public int QuestionTypeId { get; set; }

        public int StatusId { get; set; }

        public int SortOrder { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
