﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class RatingByComment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public byte Rating { get; set; }
    }
}
