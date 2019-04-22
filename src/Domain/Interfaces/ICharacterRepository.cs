using Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;

namespace Domain.Interfaces
{
    public interface ICharacterRepository
    {
        Task<List<CharacterDto>> GetAllCharacters();
        Task<CharacterDto> GetCharacter(int characterId);
        Task AddCharacters(List<Character> characters);
        Task UpdateCharacter(int characterId, Character character);
        Task DeleteCharacter(int characterId);
    }
}