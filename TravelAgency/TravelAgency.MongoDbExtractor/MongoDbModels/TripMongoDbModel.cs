using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.MongoDbExtractor.MongoDbModels
{
    public class TripMongoDbModel
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("arrivalDate")]
        public DateTime ArrivalDate { get; set; }

        [BsonElement("destinations")]
        public IEnumerable<DestinationMongoDbModel> Destinations { get; set; }
    }
}
