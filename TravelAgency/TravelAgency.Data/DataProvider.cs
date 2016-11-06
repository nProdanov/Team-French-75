using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data.Repositories;
using TravelAgency.Models;

namespace TravelAgency.Data
{
    public class DataProvider
    {
        private ITravelAgencyDbContext context;
        private IRepository<Customer> customerRepo;
        private IRepository<Destination> destinationRepo;
        private IRepository<Touroperator> toureoperatorRepo;
        private IRepository<Trip> tripRepo;

        public DataProvider(ITravelAgencyDbContext context, IRepository<Customer> customer,
                               IRepository<Destination> destination, IRepository<Touroperator> touroperator,
                               IRepository<Trip> trip)
        {
            this.context = context;
            this.customerRepo = customer;
            this.destinationRepo = destination;
            this.toureoperatorRepo = touroperator;
            this.tripRepo = trip;
        }

        public ITravelAgencyDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<Customer> CustomerRepo { get { return this.customerRepo; } }
        public IRepository<Destination> DestinationRepo { get { return this.DestinationRepo; } }
        public IRepository<Touroperator> ToureoperatorRepo { get { return this.toureoperatorRepo; } }
        public IRepository<Trip> TripRepo { get { return this.tripRepo; } }
    }
}
