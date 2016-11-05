using System.Collections.Generic;

using TravelAgency.MySqlData;

namespace TravelAgency.Readers.Contracts
{
    public interface IMySqlReader
    {
        ICollection<TripReportMySqlModel> ReadMySql();
    }
}