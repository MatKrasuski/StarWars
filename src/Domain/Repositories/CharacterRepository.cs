using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        public List<CharacterDto> GetAllCharacters()
        {
            return Characters;
        }

        public CharacterDto GetCharacter(int characterId)
        {
            return Characters.FirstOrDefault(c => c.Id == characterId);
        }

        List<CharacterDto> Characters => new List<CharacterDto>
        {
            new CharacterDto
            {
                Id = 1,
                Name = "Luke",
                Episodes = new[]{ "NEWHOPE", "EMPIRE", "JEDI" },
                Planet = "Tatooine",
                Friends = new[]{ "Han Solo", "Leia Organa", "C - 3PO", "R2 - D2" }
            },

            new CharacterDto
            {
                Id = 2,
                Name = "Darth Vader",
                Episodes = new[]{ "NEWHOPE", "EMPIRE", "JEDI" },
                Friends = new[]{ "Wilhuff Tarkin" }
            },

            new CharacterDto
            {
                Id = 3,
                Name = "Han Solo",
                Episodes = new[]{ "NEWHOPE", "EMPIRE", "JEDI" },
                Friends = new[]{ "Luke Skywalker", "Leia Organa", "R2-D2" }
            },
        };
    }
}
