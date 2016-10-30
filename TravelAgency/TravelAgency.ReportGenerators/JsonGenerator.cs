using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class JsonGenerator : IReportGenerator
    {
        public void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext)
        {
            string folderPath = "../../../../Json-Reports/";
            Directory.CreateDirectory(folderPath);
            var trips =
            (from t in travelAgencyDbContext.Trips
             join oper in travelAgencyDbContext.Touroperators
             on t.Touroperator equals oper
             select new
             {
                 TripID = t.Id,
                 TripName = t.Name,
                 TourOperator = oper.Name,
             }).ToList();

            JsonSerializer serializer = new JsonSerializer();
            for (int i = 0; i < trips.Count; i++)
            {
                var filename = string.Format("{0}.json", i + 1);
                using (StreamWriter sw = new StreamWriter(folderPath + filename))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, trips[i]);

                }
            }

        }
    }
}
