using System.Collections.Generic;

using Telerik.OpenAccess.Metadata.Fluent;

namespace TravelAgency.Importers.MySqlUtils
{
    public partial class MySqlModelMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            var configurations = new List<MappingConfiguration>();

            var tripReportMapping = new MappingConfiguration<TripReport>();
            tripReportMapping.MapType(tripReport => new
            {
                ID = tripReport.ID,
                Name = tripReport.TripName,
                Touroperator = tripReport.TouroperatorName,
                TotalTripsSold = tripReport.TotalTripsSold,
                TotalIncomes = tripReport.TotalIncomes
            }).ToTable("TripsReports");
            tripReportMapping.HasProperty(c => c.ID).IsIdentity();

            configurations.Add(tripReportMapping);

            return configurations;
        }
    }
}
