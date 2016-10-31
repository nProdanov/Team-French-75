using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var trips = (from t in travelAgencyDbContext.Trips
                         join oper in travelAgencyDbContext.Touroperators
                         on t.Touroperator equals oper
                         select new
                         {
                             TripID = t.Id,
                             TripName = t.Name,
                             TourOperator = oper.Name,
                             tripPrice= t.Price,
                             tripDiscount=t.Discount
                         }).ToList();
            //var NumberOfTrips = travelAgencyDbContext.Trips.Count();

            JsonSerializer serializer = new JsonSerializer();
            for (int i = 0; i < trips.Count; i++)
            {
                var totalTripsSold = 0;
                decimal totalAmountPerTrip = 0;
                var cusommersPerTrip = (from customers in travelAgencyDbContext.Customers
                    where customers.Trips.Any(c => c.Id == i+1)
                    select customers).ToList();

                foreach (var customer in cusommersPerTrip)
                {
                    totalTripsSold += 1;
                    if (customer.HasDiscount)
                    {
                        totalAmountPerTrip += trips[i].tripPrice-(trips[i].tripPrice* (decimal)trips[i].tripDiscount/100);
                    }
                    else
                    {
                        totalAmountPerTrip += trips[i].tripPrice;
                    }
                    
                }

                JObject json = JObject.FromObject(trips[i]);
                json.Add("TotalTripsSold", totalTripsSold);
                json.Add("TotalIncomes", totalAmountPerTrip);
                json.Remove("tripPrice");
                json.Remove("tripDiscount");
                var filename = string.Format("{0}.json", i + 1);
                using (StreamWriter sw = new StreamWriter(folderPath + filename))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, json);

                }
            }

        }
    }
}
