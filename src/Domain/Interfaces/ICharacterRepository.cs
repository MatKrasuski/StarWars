using Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICharacterRepository
    {
        Task<List<CharacterDto>> GetAllCharacters();
        Task<CharacterDto> GetCharacter(string characterId);
    }
}