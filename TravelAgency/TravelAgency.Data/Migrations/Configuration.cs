using System.Data.Entity.Migrations;

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
        }
    }
}
