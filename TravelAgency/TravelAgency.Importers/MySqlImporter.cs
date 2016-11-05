using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;
using Newtonsoft.Json;
using TravelAgency.Readers.Contracts;
using TravelAgency.MySqlData;
using TravelAgency.ParseModels;
using TravelAgency.Common;

namespace TravelAgency.Importers
{
    public class MySqlImporter
    {
        private const string ConnectionString = Constants.MySQLConnectionString;

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

        private IEnumerable<TripReportMySqlModel> MapTripReportsFromJson()
        {
            var jsonReports = this.jsonFileReportReader.ReadJsonReports();

            var tripReports = 
                jsonReports
                .Select(json => JsonConvert.DeserializeObject<TripReportParseModel>(json))
                .Select(jsonTrip => new TripReportMySqlModel()
                {
                    ID = jsonTrip.ID,
                    TripName = jsonTrip.TripName,
                    TotalIncomes = jsonTrip.TotalIncomes,
                    TotalTripsSold = jsonTrip.TotalTripsSold,
                    TouroperatorName = jsonTrip.TouroperatorName
                });

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
