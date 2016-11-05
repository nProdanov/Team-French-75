using System.Collections.Generic;
using System.Linq;

using TravelAgency.Common;
using TravelAgency.MySqlData;
using TravelAgency.Readers.Contracts;

namespace TravelAgency.Readers
{
    public class MySqlReader : IMySqlReader
    {
        private const string ConnectionString = Constants.MySQLConnectionString;

        public ICollection<TripReportMySqlModel> ReadMySql()
        {
            using (var context = new MySqlContext(ConnectionString)) 
            {
                return context.TripsReports.ToList();
            }
        }
    }
}
