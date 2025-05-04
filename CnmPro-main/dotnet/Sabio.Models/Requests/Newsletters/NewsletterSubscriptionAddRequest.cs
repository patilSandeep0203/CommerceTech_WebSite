﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Newsletters
{
    public class NewsletterSubscriptionAddRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsSubscribed { get; set; }
    }
}
