using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;

namespace API.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAllCharacters();
        Task<Character> GetCharacter(string characterId);
        Task AddCharacter(CharacterBase character);
        Task UpdateCharacter(string id, CharacterBase character);
    }
}