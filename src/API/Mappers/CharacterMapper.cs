using System.Collections.Generic;
using Bussiness.Models;
using Domain.Dtos;

namespace API.Mappers
{
    public class CharacterMapper : ICharacterMapper
    {
        public List<Character> MapCharacters(List<CharacterDto> charactersDto)
        {
            var characters = new List<Character>();

            foreach (var characterDto in charactersDto)
            {
                var character = new  Character
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

        public Character MapSingleCharacter(CharacterDto characterDto)
        {
            return  new Character
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