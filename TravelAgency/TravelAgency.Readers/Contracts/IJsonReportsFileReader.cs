using System.Collections.Generic;

namespace TravelAgency.Readers.Contracts
{
    public interface IJsonReportsFileReader
    {
        IEnumerable<string> ReadJsonReports();
    }
}