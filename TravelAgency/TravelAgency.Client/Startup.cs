using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TravelAgency.Data;
using TravelAgency.Data.Migrations;
using TravelAgency.MongoDbExtractor;
using TravelAgency.Readers;

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
                var excelReader = new ExcelReader();
                var dataImporter = new TravelAgenciesDataImporter(travelAgencyDbContext, mongoExtractor, excelReader);

                dataImporter.ImportData();
                travelAgencyDbContext.SaveChanges();

                // generate report 1 - pdf
                // generate report 2 - json - save to mysql
                // generate report 3 - xml
                // generate report 4 - export to mysql

                // read mysql reports - read sqlite - generate excel
            }
            //// Just for test - to see if something has been written to the Database
            // Console.WriteLine(db.Destinations.Count());
        }
    }
}
