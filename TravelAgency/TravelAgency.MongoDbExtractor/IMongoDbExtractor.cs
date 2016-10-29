using System.Collections.Generic;

using TravelAgency.ParseModels;

namespace TravelAgency.MongoDbExtractor
{
    public interface IMongoDbExtractor
    {
        IEnumerable<TouroperatorMongoDbModel> ExtractMongoDbTourOperators();
    }
}
