﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain
{
    public class Location
    {
        public int Id { get; set;  }
        public LookUp LocationType { get; set; } 
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public State State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

    }
}
