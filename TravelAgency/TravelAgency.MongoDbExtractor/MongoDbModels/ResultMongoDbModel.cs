using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.MongoDbExtractor.MongoDbModels
{
    [BsonDiscriminator("result")]
    public class ResultMongoDbModel
    {
        [BsonElement("touroperators")]
        public IEnumerable<TouroperatorMongoDbModel> Touroperators { get; set; }
    }
}
