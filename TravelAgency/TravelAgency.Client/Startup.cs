using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TravelAgency.Data;
using TravelAgency.Data.Migrations;
using TravelAgency.Importers;
using TravelAgency.Readers;
using TravelAgency.ReportGenerators;

namespace TravelAgency.Client
{
    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TravelAgencyDbContext, Configuration>());

            using (var travelAgencyDbContext = new TravelAgencyDbContext())
            {
                var mongoReader = new MongoReader();
                var excelReader = new ExcelReader();
                var xmlReader = new XmlReader();
                var dataImporter = new TravelAgenciesDataImporter(travelAgencyDbContext, mongoReader, excelReader, xmlReader);

                dataImporter.ImportGeneralData();
                travelAgencyDbContext.SaveChanges();

                dataImporter.ImportAdditionalData();
                travelAgencyDbContext.SaveChanges();

                var mongoDataImporter = new MongoImporter(xmlReader);
                mongoDataImporter.ImportDiscounts();

                var reportersFactory = new ReportGeneratorsFactory();

                while (true)
                {
                    Console.WriteLine("Please write the type of report you want to be generated - xml, json, pdf.");

                    var reporterName = Console.ReadLine().ToLower();

                    try
                    {
                        var reporter = reportersFactory.CreateReportGenerator(reporterName);
                        reporter.GenerateReport(travelAgencyDbContext);
                        Console.WriteLine($"The {reporterName} report has been successfully generated.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

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
