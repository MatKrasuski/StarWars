using MongoDB.Bson;

namespace API.Models
{
    public class Character
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Episodes { get; set; }
        public string Planet { get; set; }
        public string[] Friends { get; set; }
    }
}
