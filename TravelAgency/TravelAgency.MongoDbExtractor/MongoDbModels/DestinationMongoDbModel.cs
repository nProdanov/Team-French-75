using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.MongoDbExtractor.MongoDbModels
{
    public class DestinationMongoDbModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
