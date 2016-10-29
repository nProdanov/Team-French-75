using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.ParseModels
{
    public class TripMongoDbModel
    {
        public TripMongoDbModel()
        {
            // TODO: check how this is going to work with mongodb
            this.Customers = new List<CustomerParseModel>();
        }

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

        public ICollection<CustomerParseModel> Customers { get; set; }
    }
}
