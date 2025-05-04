﻿using Sabio.Models.Requests.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class Message 
    {
         
        public int Id { get; set; }
       
        public string MessageText { get; set; }
     
        public string Subject { get; set; }
     
        public int RecipientId { get; set; }
      
        public int SenderId { get; set; }

        public List<MessageUserBase> SenderData { get; set; }

        public List<MessageUserBase> RecipientData { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime DateRead { get; set; }
       
        public DateTime DateCreated { get; set; }
      
        public DateTime DateModified { get; set; }
    }
}
