using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.ParseModels
{
    public class TouroperatorParseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public TouroperatorParseModel()
        {
            this.Customers = new List<CustomerParseModel>();
        }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("trips")]
        public IEnumerable<TripParseModel> Trips { get; set; }

        public ICollection<CustomerParseModel> Customers { get; set; }
    }
}
