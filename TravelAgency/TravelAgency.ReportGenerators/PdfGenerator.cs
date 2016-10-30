using System;
using TravelAgency.Data;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class PdfGenerator : IReportGenerator
    {
        public void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext)
        {
            throw new NotImplementedException();
        }
    }
}
