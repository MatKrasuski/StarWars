using System.Collections.Generic;
using System.Linq;
using Bussiness.Models;
using Domain.Dtos;

namespace API.Mappers
{
    public class CharacterMapper : ICharacterMapper
    {
        public List<Character> MapCharacters(Dictionary<int, CharacterDto> charactersDto)
        {
            var characters = new List<Character>();

            foreach (var characterDto in charactersDto)
            {
                var character = new  Character
                {
                    CharacterId = characterDto.Key,
                    Planet = characterDto.Value.Planet,
                    Name = characterDto.Value.Name,
                    Episodes = characterDto.Value.Episodes.Select(s => s.EpisodeName).ToArray(),
                    Friends = characterDto.Value.Friends.Select(s => s.FriendName).ToArray()
                };

                characters.Add(character);
            }

            return characters;
        }

        public Character MapSingleCharacter(KeyValuePair<int, CharacterDto> characterDto)
        {
            return  new Character
            {
                CharacterId = characterDto.Key,
                Name = characterDto.Value.Name,
                Planet = characterDto.Value.Planet,
                Episodes = characterDto.Value.Episodes.Select(s => s.EpisodeName).ToArray(),
                Friends = characterDto.Value.Friends.Select(s => s.FriendName).ToArray()

            };
        }

        public List<CharacterDto> MapCaractersToDtos(List<Character> characters)
        {
            var charactersDto = new List<CharacterDto>();

            foreach (var characterItem in characters)
            {
                var character = new CharacterDto
                {
                    CharacterId = characterItem.CharacterId,
                    Planet = characterItem.Planet,
                    Name = characterItem.Name,
                    Episodes = characterItem.Episodes.Select(x => new Episode{EpisodeName = x}).ToList(),
                    Friends = characterItem.Friends.Select(x => new Friend { FriendName = x }).ToList()
                };

                charactersDto.Add(character);
            }

            return charactersDto;
        }

        public CharacterDto MapSingleCaracterToDto(int characterId, Character character)
        {
            return new CharacterDto
            {
                CharacterId = characterId,
                Planet = character.Planet,
                Name = character.Name,
                Episodes = character.Episodes.Select(x => new Episode { EpisodeName = x }).ToList(),
                Friends = character.Friends.Select(x => new Friend { FriendName = x }).ToList()
            };
        }
    }
}