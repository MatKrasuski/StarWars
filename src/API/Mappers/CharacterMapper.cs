using System.Collections.Generic;
using System.Linq;
using Bussiness.Models;
using Domain.Dtos;

namespace API.Mappers
{
    public class CharacterMapper : ICharacterMapper
    {
        public List<CharacterBase> MapCharacters(List<CharacterDto> charactersDto)
        {
            var characters = new List<CharacterBase>();

            foreach (var characterDto in charactersDto)
            {
                var character = new  CharacterBase
                {
                    Id = characterDto.CharacterId,
                    Episodes = characterDto.Episodes.Split('|').ToArray(),
                    Planet = characterDto.Planet,
                    Name = characterDto.Name,
                    Friends = characterDto.Friends.Split('|').ToArray()
                };

                characters.Add(character);
            }

            return characters;
        }

        public CharacterBase MapSingleCharacter(CharacterDto characterDto)
        {
            return  new CharacterBase
            {
                Friends = characterDto.Friends.Split('|').ToArray(),
                Name = characterDto.Name,
                Planet = characterDto.Planet,
                Episodes = characterDto.Episodes.Split('|').ToArray(),
                Id = characterDto.CharacterId
            };
        }
    }
}