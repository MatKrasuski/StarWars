using System.Collections.Generic;

namespace Bussiness.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Planet { get; set; }
        public string Name { get; set; }
        public string[] Episodes { get; set; }
        public string[] Friends { get; set; }
    }
}

