using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.ParseModels
{
    public class DestinationParseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
