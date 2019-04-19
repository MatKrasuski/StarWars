using Domain.Dtos;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ICharacterRepository
    {
        List<CharacterDto> GetAllCharacters();
        CharacterDto GetCharacter(int characterId);
    }
}