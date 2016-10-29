using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.ParseModels
{
    public class TouroperatorMongoDbModel
    {
        public TouroperatorMongoDbModel()
        {
            this.Customers = new List<CustomerParseModel>();
        }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("trips")]
        public IEnumerable<TripMongoDbModel> Trips { get; set; }

        public ICollection<CustomerParseModel> Customers { get; set; }
    }
}
