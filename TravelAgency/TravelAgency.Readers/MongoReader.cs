using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;

using TravelAgency.ParseModels;
using TravelAgency.Readers.Contracts;

namespace TravelAgency.Readers
{
    public class MongoReader : IMongoReader
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "travelagency";
        private const string CollectionName = "touroperators";

        public IEnumerable<TouroperatorParseModel> ReadMongo()
        {
            var mongoDb = this.GetMongoDb();
            var collection = mongoDb.GetCollection<TouroperatorParseModel>(CollectionName);

            var result = collection.Find(t => true).ToList();

            return result;
        }

        private IMongoDatabase GetMongoDb()
        {
            var mongoDbClient = new MongoClient(ConnectionString);
            return mongoDbClient.GetDatabase(DatabaseName);
        }
    }
}
