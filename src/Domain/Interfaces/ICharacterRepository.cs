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
        Task AddCharacters(List<CharacterDto> characters);
        Task UpdateCharacter(int characterId, CharacterDto character);
        Task DeleteCharacter(int characterId);
    }
}