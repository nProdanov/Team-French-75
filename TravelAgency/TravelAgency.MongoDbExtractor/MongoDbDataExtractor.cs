using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;

using TravelAgency.ParseModels;

namespace TravelAgency.MongoDbExtractor
{
    public class MongoDbDataExtractor : IMongoDbExtractor
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "travelagency";
        private const string CollectionName = "touroperators";

        public IEnumerable<TouroperatorMongoDbModel> ExtractMongoDbTourOperators()
        {
            var mongoDb = this.GetMongoDb();
            var collection = mongoDb.GetCollection<SchemaMongoDbModel>(CollectionName);

            var result = collection
                .Find(t => true)
                .FirstOrDefault()
                .Result;

            return result.Touroperators;
        }

        private IMongoDatabase GetMongoDb()
        {
            var mongoDbClient = new MongoClient(ConnectionString);
            return mongoDbClient.GetDatabase(DatabaseName);
        }
    }
}
