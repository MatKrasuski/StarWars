using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bussiness.Models
{
    public class CharacterBase : Character
    {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
    }
}
