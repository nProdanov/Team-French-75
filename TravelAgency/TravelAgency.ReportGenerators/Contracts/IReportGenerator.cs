using TravelAgency.Data;

namespace TravelAgency.ReportGenerators.Contracts
{
    public interface IReportGenerator
    {
        void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext);
    }
}
