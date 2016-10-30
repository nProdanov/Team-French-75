using Telerik.OpenAccess;
using TravelAgency.Importers.MySqlUtils;

namespace TravelAgency.Importers
{
    class MySqlImporter
    {
        private const string ConnectionString = "server=localhost;database=travelagency;uid=root;pwd=1234;";

        public void ImportTripsReports()
        {
            using (var context = new MySqlContext(ConnectionString))
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);

                //context.Add(tripsReportsToAdd);
                context.SaveChanges();
            }
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
