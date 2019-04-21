using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Mappers;
using Bussiness.Models;

namespace API.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ICharacterMapper _characterMapper;

        public CharacterService(ICharacterRepository characterRepository, ICharacterMapper characterMapper)
        {
            _characterRepository = characterRepository;
            _characterMapper = characterMapper;
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            var charactersDto =  await _characterRepository.GetAllCharacters();
            return _characterMapper.MapCharacters(charactersDto);
        }

        public async Task<Character> GetCharacter(string characterId)
        {
            var characterDto =  await _characterRepository.GetCharacter(characterId);

            if (characterDto == null)
            {
                return new NullCharacter();
            }

            return _characterMapper.MapSingleCharacter(characterDto);
        }

        public async Task AddCharacter(CharacterBase character)
        {
            await _characterRepository.AddCharacter(character);
        }

        public async  Task UpdateCharacter(string id, CharacterBase character)
        {
            await _characterRepository.UpdateCharacter(id, character);
        }

        public async Task DeleteCharacter(string id)
        {
            await _characterRepository.DeleteCharacter(id);
        }
    }
}
