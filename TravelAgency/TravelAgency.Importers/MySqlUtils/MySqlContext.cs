using System.Linq;

using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace TravelAgency.Importers.MySqlUtils
{
    public partial class MySqlContext : OpenAccessContext
    {
        private static BackendConfiguration backend = GetBackendConfiguration();
        private static MetadataSource metadataSource = new MySqlModelMetadataSource();

        public MySqlContext(string connectionString)
            : base(connectionString, backend, metadataSource)
        {
        }

        public IQueryable<TripReportParseModel> TripsReports
        {
            get
            {
                return this.GetAll<TripReportParseModel>();
            }
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration()
            {
                Backend = "MySql",
                ProviderName = "MySql.Data.MySqlClient"
            };

            return backend;
        }

    }
}
