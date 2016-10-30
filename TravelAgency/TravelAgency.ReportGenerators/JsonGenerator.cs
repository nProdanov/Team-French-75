using System;
using TravelAgency.Data;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class JsonGenerator : IReportGenerator
    {
        public void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext)
        {
            throw new NotImplementedException();
        }
    }
}
