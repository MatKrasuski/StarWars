using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bussiness.Models
{
    public class Character : CharacterBase
    {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
    }
}
