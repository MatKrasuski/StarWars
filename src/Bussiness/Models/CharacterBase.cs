using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bussiness.Models
{
    public class CharacterBase : Character
    {
        public int Id { get; set; }
        
    }
}
