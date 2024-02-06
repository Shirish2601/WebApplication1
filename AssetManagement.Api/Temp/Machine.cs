using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetManagement.Api.Temp
{
    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("machineName")]
        public string MachineName { get; set; }

        [BsonElement("assets")]
        public List<Asset> Assets { get; set; }
    }
}
