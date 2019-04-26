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

        public async Task<Character> GetCharacter(int characterId)
        {
            var characterDto =  await _characterRepository.GetCharacter(characterId);

            if (characterDto == null)
            {
                return new NullCharacter();
            }

            return _characterMapper.MapSingleCharacter(characterDto);
        }

        public async Task AddCharacters(List<Character> characters)
        {
            var characterDtos = _characterMapper.MapCaractersToDtos(characters);
            await _characterRepository.AddCharacters(characterDtos);
        }

        public async  Task UpdateCharacter(int characterId, Character character)
        {
            var characterDto = _characterMapper.MapSingleCaracterToDto(characterId, character);
            await _characterRepository.UpdateCharacter(characterDto);
        }

        public async Task DeleteCharacter(int characterId)
        {
            await _characterRepository.DeleteCharacter(characterId);
        }
    }
}
