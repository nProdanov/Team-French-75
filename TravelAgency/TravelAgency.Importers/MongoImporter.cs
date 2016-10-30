using System.Linq;

using MongoDB.Driver;

using TravelAgency.ParseModels;
using TravelAgency.Readers.Contracts;

namespace TravelAgency.Importers
{
    public class MongoImporter
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "travelagency";
        private const string CollectionName = "touroperators";

        private IXmlReader xmlReader;

        public MongoImporter(IXmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
        }

        public void ImportDiscounts()
        {
            var discounts = this.xmlReader.ReadXml();

            var mongoDb = this.GetMongoDb();
            var touroperators = mongoDb.GetCollection<TouroperatorParseModel>(CollectionName);

            foreach (var disc in discounts)
            {
                var tripName = disc.Key;
                var tripDiscount = disc.Value;

                var filter = Builders<TouroperatorParseModel>.Filter.Where(t => t.Trips.Any(tr => tr.Name == tripName));
                var update = Builders<TouroperatorParseModel>.Update.Set(t => t.Trips.ElementAt(-1).Discount, tripDiscount);
                var result = touroperators.UpdateOneAsync(filter, update).Result;
            }
        }

        private IMongoDatabase GetMongoDb()
        {
            var mongoDbClient = new MongoClient(ConnectionString);
            return mongoDbClient.GetDatabase(DatabaseName);
        }
    }
}
