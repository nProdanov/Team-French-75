using System;
using TravelAgency.Data.Repositories;
using TravelAgency.Data;
using TravelAgency.Importers;
using TravelAgency.Models;
using TravelAgency.Readers;
using TravelAgency.ReportGenerators;

namespace TravelAgency.Client
{
    public class Startup
    {
        public static void Main()
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<TravelAgencyDbContext, Configuration>());

            using (var travelAgencyDbContext = new TravelAgencyDbContext())
            {
                var sqlData = new DataProvider(travelAgencyDbContext, new EfGenericRepostitory<Customer>(travelAgencyDbContext),
                                               new EfGenericRepostitory<Destination>(travelAgencyDbContext),
                                               new EfGenericRepostitory<Touroperator>(travelAgencyDbContext),
                                               new EfGenericRepostitory<Trip>(travelAgencyDbContext));
                var mongoReader = new MongoReader();
                var excelReader = new ExcelReader();
                var xmlReader = new XmlReader();
              
                var dataImporter = new SqlImporter(sqlData, mongoReader, excelReader, xmlReader);
                var jsonReader = new JsonReportsFileReader();
                var mySqlImporter = new MySqlImporter(jsonReader);
                dataImporter.ImportGeneralData();

                dataImporter.ImportAdditionalData();

                var mongoDataImporter = new MongoImporter(xmlReader);
                mongoDataImporter.ImportDiscounts();

                var reportersFactory = new ReportGeneratorsFactory(mySqlImporter);

                while (true)
                {
                    Console.WriteLine("Please write the type of report you want to be generated - xml, json, pdf, xlsx.");
                
                    var reporterName = Console.ReadLine().ToLower();

                    // TODO: find another way
                    if (reporterName == "xlsx")
                    {
                        var excel = new ExcelGenerator();
                        excel.GenerateReport();
                    }
                    else
                    {
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
                }
            }
        }
    }
}
