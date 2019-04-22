using Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;

namespace Domain.Interfaces
{
    public interface ICharacterRepository
    {
        Task<List<CharacterDto>> GetAllCharacters();
        Task<CharacterDto> GetCharacter(string characterId);
        Task AddCharacters(List<Character> character);
        Task UpdateCharacter(string characterId, Character character);
        Task DeleteCharacter(string characterId);
    }
}