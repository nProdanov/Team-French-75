using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Importers;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class JsonGenerator : IReportGenerator
    {
        private MySqlImporter mySqlImporter;

        public JsonGenerator(MySqlImporter importer)
        {
            this.mySqlImporter = importer;
        }
        public void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext)
        {
            string folderPath = "../../../../Json-Reports/";
            Directory.CreateDirectory(folderPath);
            var trips = travelAgencyDbContext.Trips.AsEnumerable()
                .Select(tr => new
                {
                    TripID = tr.Id,
                    TripName = tr.Name,
                    TourOperator = tr.Touroperator.Name,
                    TotalTripsSold = tr.Customers.Count,
                    TotalIncomes = tr.Customers.Count() * (tr.Price) - (decimal)(tr.Customers.Count(c => c.HasDiscount) * tr.Discount / 100) * (tr.Price)

                }).ToList();
            JsonSerializer serializer = new JsonSerializer();
            var fileSequence = 1;
            foreach (var trip in trips)
            {

                var filename = string.Format("{0}.json", fileSequence);
                fileSequence++;
                using (StreamWriter sw = new StreamWriter(folderPath + filename))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, trip);

                }
            }

            mySqlImporter.ImportTripsReports();
        }
    }
}
