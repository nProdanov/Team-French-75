using System.Collections.Generic;

using TravelAgency.MongoDbExtractor.MongoDbModels;

namespace TravelAgency.MongoDbExtractor
{
    public interface IMongoDbExtractor
    {
        IEnumerable<TouroperatorMongoDbModel> ExtractMongoDbTourOperators();
    }
}
