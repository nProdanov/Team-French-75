using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.ParseModels
{
    public class TripMongoDbModel
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        public DateTime DepartureDate { get; set; }

        [BsonElement("arrivalDate")]
        public DateTime ArrivalDate { get; set; }

        public float Discount { get; set; }

        [BsonElement("destinations")]
        public IEnumerable<DestinationMongoDbModel> Destinations { get; set; }

        public IEnumerable<Customer> Customers { get; set; } 
    }
}
