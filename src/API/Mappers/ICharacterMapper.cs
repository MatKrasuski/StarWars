using System.Collections.Generic;
using API.Models;
using Domain.Dtos;

namespace API.Mappers
{
    public interface ICharacterMapper
    {
        List<Character> MapCharacters(List<CharacterDto> charactersDto);
        Character MapSingleCharacter(CharacterDto characterDto);
    }
}