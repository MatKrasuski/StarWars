using System.Collections.Generic;
using Bussiness.Models;
using Domain.Dtos;

namespace API.Mappers
{
    public interface ICharacterMapper
    {
        List<Character> MapCharacters(Dictionary<int, CharacterDto> charactersDto);
        Character MapSingleCharacter(KeyValuePair<int, CharacterDto> characterDto);
        List<CharacterDto> MapCaractersToDtos(List<Character> characters);
        CharacterDto MapSingleCaracterToDto(int characterId, Character character);
    }
}