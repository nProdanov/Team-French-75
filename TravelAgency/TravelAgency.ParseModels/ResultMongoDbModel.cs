using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAgency.ParseModels
{
    [BsonDiscriminator("result")]
    public class ResultMongoDbModel
    {
        [BsonElement("touroperators")]
        public IEnumerable<TouroperatorMongoDbModel> Touroperators { get; set; }
    }
}
