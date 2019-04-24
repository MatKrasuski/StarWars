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
                    CharacterId = characterDto.CharacterId,
                    Planet = characterDto.Planet,
                    Name = characterDto.Name,
                    Episodes = characterDto.Episodes,
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
                CharacterId = characterDto.CharacterId,
                Name = characterDto.Name,
                Planet = characterDto.Planet,
                Friends = characterDto.Friends,
                Episodes = characterDto.Episodes
                
            };
        }
    }
}