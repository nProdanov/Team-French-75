using Newtonsoft.Json;

namespace TravelAgency.Importers.MySqlUtils
{
    public class TripReport
    {
        [JsonIgnore]
        public int ID { get; set; }

        [JsonProperty("tripName")]
        public string TripName { get; set; }

        [JsonProperty("touroperator")]
        public string TouroperatorName { get; set; }

        [JsonProperty("totalTripsSold")]
        public int TotalTripsSold { get; set; }

        [JsonProperty("totalIncomes")]
        public decimal TotalIncomes { get; set; }
    }
}
