using Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICharacterRepository
    {
        Task<Dictionary<int, CharacterDto>> GetAllCharacters();
        Task<KeyValuePair<int, CharacterDto>> GetCharacter(int characterId);
        Task AddCharacters(List<CharacterDto> characters);
        Task UpdateCharacter(CharacterDto character);
        Task DeleteCharacter(int characterId);
    }
}