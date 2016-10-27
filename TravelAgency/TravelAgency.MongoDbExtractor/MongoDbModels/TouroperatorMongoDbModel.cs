using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.MongoDbExtractor.MongoDbModels
{
    public class TouroperatorMongoDbModel
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("trips")]
        public IEnumerable<TripMongoDbModel> Trips { get; set; }
    }
}
