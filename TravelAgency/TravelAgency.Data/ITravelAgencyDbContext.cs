using System.Data.Entity;

using TravelAgency.Models;

namespace TravelAgency.Data
{
    public interface ITravelAgencyDbContext
    {
        IDbSet<Customer> Customers { get; set; }

        IDbSet<Destination> Destinations { get; set; }

        IDbSet<Touroperator> Touroperators { get; set; }

        IDbSet<Trip> Trips { get; set; }

        int SaveChanges();
    }
}