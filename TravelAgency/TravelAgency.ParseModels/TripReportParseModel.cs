﻿using Newtonsoft.Json;

namespace TravelAgency.ParseModels
{
    public class TripReportParseModel
    {
        [JsonProperty("TripID")]
        public int ID { get; set; }

        [JsonProperty("TripName")]
        public string TripName { get; set; }

        [JsonProperty("TourOperator")]
        public string TouroperatorName { get; set; }

        [JsonProperty("TotalTripsSold")]
        public int TotalTripsSold { get; set; }

        [JsonProperty("TotalIncomes")]
        public decimal TotalIncomes { get; set; }
    }
}
