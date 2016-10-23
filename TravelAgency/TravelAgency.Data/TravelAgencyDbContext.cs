﻿using System.Data.Entity;
using TravelAgency.Models;

namespace TravelAgency.Data
{
    public class TravelAgencyDbContext : DbContext
    {
        public TravelAgencyDbContext()
            : base("TravelAgencyDatabase")
        {
        }

        public virtual IDbSet<Customer> Customers { get; set; }

        public virtual IDbSet<Destination> Destinations { get; set; }

        public virtual IDbSet<Touroperator> Touroperators { get; set; }

        public virtual IDbSet<Trip> Trips { get; set; }
    }
}
