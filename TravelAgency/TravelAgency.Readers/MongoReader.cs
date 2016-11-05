using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;

using TravelAgency.ParseModels;
using TravelAgency.Readers.Contracts;
using TravelAgency.Common;

namespace TravelAgency.Readers
{
    public class MongoReader : IMongoReader
    {
        private const string ConnectionString = Constants.MongoDbConnectionString;
        private const string DatabaseName = Constants.MongoDbDatabaseName;
        private const string CollectionName = Constants.MongoDbCollectionName;

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
