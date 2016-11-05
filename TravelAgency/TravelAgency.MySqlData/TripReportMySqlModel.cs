﻿namespace TravelAgency.MySqlData
{
    public class TripReportMySqlModel
    {
        public int ID { get; set; }
        
        public string TripName { get; set; }
        
        public string TouroperatorName { get; set; }
        
        public int TotalTripsSold { get; set; }
        
        public decimal TotalIncomes { get; set; }
    }
}
