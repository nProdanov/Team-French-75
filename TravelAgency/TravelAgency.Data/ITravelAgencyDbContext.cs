using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TravelAgency.Models;

namespace TravelAgency.Data
{
    public interface ITravelAgencyDbContext
    {
        IDbSet<Customer> Customers { get; set; }

        IDbSet<Destination> Destinations { get; set; }

        IDbSet<Touroperator> Touroperators { get; set; }

        IDbSet<Trip> Trips { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();
    }
}