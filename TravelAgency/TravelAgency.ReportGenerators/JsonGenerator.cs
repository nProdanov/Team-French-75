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

            string filePath = "../../../../Json-Reports/";
            var trips = travelAgencyDbContext.Trips.ToList();
            JsonSerializer serializer = new JsonSerializer();
            for (int i = 0; i < trips.Count; i++)
            {
                var filename = string.Format("{0}.json", i);
                using (StreamWriter sw = new StreamWriter(filePath + filename))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, trips[i]);
                   
                }
            }
        }
    }
}
