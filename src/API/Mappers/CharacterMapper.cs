using System.Collections.Generic;
using System.Linq;
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
                    Episodes = characterDto.Episodes.Select(s => s.EpisodeName).ToArray(),
                    Friends = characterDto.Friends.Select(s => s.FriendName).ToArray()
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
                Episodes = characterDto.Episodes.Select(s => s.EpisodeName).ToArray(),
                Friends = characterDto.Friends.Select(s => s.FriendName).ToArray()

            };
        }

        public List<CharacterDto> MapCaractersToDto(List<Character> characters)
        {
            var charactersDto = new List<CharacterDto>();

            foreach (var characterItem in characters)
            {
                var character = new CharacterDto
                {
                    CharacterId = characterItem.CharacterId,
                    Planet = characterItem.Planet,
                    Name = characterItem.Name,
                    //Episodes = characterItem.Episodes.Select(x => ),
                    //Friends = characterItem.Friends.Select(s => s.FriendName).ToArray()
                };

                charactersDto.Add(character);
            }

            return charactersDto;
        }
    }
}