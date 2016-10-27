using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.MongoDbExtractor.MongoDbModels
{
    public class SchemaMongoDbModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("result")]
        public ResultMongoDbModel Result { get; set; }
    }
}
