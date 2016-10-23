using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using TravelAgency.Models;

namespace TravelAgency.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<TravelAgency.Data.TravelAgencyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "TravelAgency.Data.TravelAgencyDbContext";
        }

        protected override void Seed(TravelAgencyDbContext context)
        {
            if (context.Destinations.Any())
            {
                return;
            }

            // Just for test perpoces - from here we will read the files and Seed the data to Database
            context.Destinations.AddOrUpdate(new Destination
            {
                Name = "Some name"
            });
        }
    }
}
