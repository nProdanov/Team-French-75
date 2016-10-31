using System.Collections.Generic;
using System.Linq;

using Telerik.OpenAccess;

using Newtonsoft.Json;

using TravelAgency.Importers.MySqlUtils;
using TravelAgency.Readers.Contracts;

namespace TravelAgency.Importers
{
    public class MySqlImporter
    {
        private const string ConnectionString = "server=localhost;database=travelagency;uid=root;pwd=Botev1912;";

        private IJsonReportsFileReader jsonFileReportReader;

        public MySqlImporter(IJsonReportsFileReader jsonFileReportReader)
        {
            this.jsonFileReportReader = jsonFileReportReader;
        }

        public void ImportTripsReports()
        {
            using (var context = new MySqlContext(ConnectionString))
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);

                var tripsReportsToAdd = MapTripReportsFromJson();
                context.Add(tripsReportsToAdd);
                context.SaveChanges();
            }
        }

        private IEnumerable<TripReport> MapTripReportsFromJson()
        {
            var jsonReports = this.jsonFileReportReader.ReadJsonReports();

            var tripReports = jsonReports.Select(json => JsonConvert.DeserializeObject<TripReport>(json));
            return tripReports;
        }  

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }
    }
}
