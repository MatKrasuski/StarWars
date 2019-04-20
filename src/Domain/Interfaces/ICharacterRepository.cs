using Domain.Dtos;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Domain.Interfaces
{
    public interface ICharacterRepository
    {
        List<CharacterDto> GetAllCharacters();
        CharacterDto GetCharacter(string characterId);
    }
}