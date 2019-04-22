using System.Collections.Generic;
using Bussiness.Models;
using Domain.Dtos;

namespace API.Mappers
{
    public interface ICharacterMapper
    {
        List<CharacterBase> MapCharacters(List<CharacterDto> charactersDto);
        CharacterBase MapSingleCharacter(CharacterDto characterDto);
    }
}