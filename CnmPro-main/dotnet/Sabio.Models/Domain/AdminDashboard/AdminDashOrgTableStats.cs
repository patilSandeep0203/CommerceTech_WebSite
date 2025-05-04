using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.AdminDashboard
{
    public class AdminDashOrgTableStats
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Headline { get; set; }
        public string Logo { get; set; }
        public string SiteUrl { get; set; }
        public string City { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string  StateCode { get; set; }
        public string Zip { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
