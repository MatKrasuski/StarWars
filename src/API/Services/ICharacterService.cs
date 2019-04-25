using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;

namespace API.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAllCharacters();
        Task<Character> GetCharacter(int characterId);
        Task AddCharacters(List<Character> character);
        Task UpdateCharacter(int id, Character character);
        Task DeleteCharacter(int characterId);
    }
}