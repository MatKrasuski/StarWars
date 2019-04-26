using System.Collections.Generic;
using Bussiness.Models;

namespace Domain.Dtos
{
    public class CharacterDto
    {
        public int CharacterId { get; set; }
        public string Planet { get; set; }
        public string Name { get; set; }
        public List<Episode> Episodes { get; set; }
        public List<Friend> Friends { get; set; }
    }
}
