using System.Collections.Generic;
using Bussiness.Models;
using Domain.Dtos;

namespace API.Mappers
{
    public interface ICharacterMapper
    {
        List<Character> MapCharacters(List<CharacterDto> charactersDto);
        Character MapSingleCharacter(CharacterDto characterDto);
        List<CharacterDto> MapCaractersToDtos(List<Character> characters);
        CharacterDto MapSingleCaracterToDto(int characterId, Character character);
    }
}