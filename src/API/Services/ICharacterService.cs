using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;

namespace API.Services
{
    public interface ICharacterService
    {
        Task<List<CharacterBase>> GetAllCharacters();
        Task<CharacterBase> GetCharacter(string characterId);
        Task AddCharacters(List<Character> character);
        Task UpdateCharacter(string id, Character character);
        Task DeleteCharacter(string id);
    }
}