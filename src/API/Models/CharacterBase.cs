using API.Validation;

namespace API.Models
{
    public class CharacterBase
    {
        public string Name { get; set; }
        public string[] Episodes { get; set; }
        public string Planet { get; set; }
        public string[] Friends { get; set; }
    }
}
