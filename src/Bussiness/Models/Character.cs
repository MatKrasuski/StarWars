using System.Collections.Generic;

namespace Bussiness.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Planet { get; set; }
        public string Name { get; set; }
        public List<Episode> Episodes { get; set; }
        public List<Friend> Friends { get; set; }
    }
}

