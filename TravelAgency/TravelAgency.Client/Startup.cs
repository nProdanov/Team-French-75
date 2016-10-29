using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TravelAgency.Data;
using TravelAgency.Data.Migrations;
using TravelAgency.MongoDbExtractor;

namespace TravelAgency.Client
{
    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TravelAgencyDbContext, Configuration>());

            using (var travelAgencyDbContext = new TravelAgencyDbContext())
            {
                var mongoExtractor = new MongoDbDataExtractor();
                var dataImporter = new TravelAgenciesDataImporter(travelAgencyDbContext, mongoExtractor);

                dataImporter.ImportData();
                travelAgencyDbContext.SaveChanges();
            }
            
            //// Just for test - to see if something has been written to the Database
            // Console.WriteLine(db.Destinations.Count());
        }
    }
}
