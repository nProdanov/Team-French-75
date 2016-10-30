using System.Collections.Generic;

using TravelAgency.ParseModels;

namespace TravelAgency.Readers.Contracts
{
    public interface IMongoReader
    {
        IEnumerable<TouroperatorParseModel> ReadMongo();
    }
}
