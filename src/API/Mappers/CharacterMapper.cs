using System.Collections.Generic;
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
                    Id = characterDto.Id.ToString(),
                    Episodes = characterDto.Episodes,
                    Planet = characterDto.Planet,
                    Name = characterDto.Name,
                    Friends = characterDto.Friends
                };

                characters.Add(character);
            }

            return characters;
        }

        public CharacterBase MapSingleCharacter(CharacterDto characterDto)
        {
            return  new CharacterBase
            {
                Friends = characterDto.Friends,
                Name = characterDto.Name,
                Planet = characterDto.Planet,
                Episodes = characterDto.Episodes,
                Id = characterDto.Id.ToString()
            };
        }
    }
}