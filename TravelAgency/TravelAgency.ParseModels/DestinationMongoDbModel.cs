using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.ParseModels
{
    public class DestinationMongoDbModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
