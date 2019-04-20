using Domain.Interfaces;
using System.Collections.Generic;
using API.Mappers;
using API.Models;
using MongoDB.Bson;

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

        public List<Character> GetAllCharacters()
        {
            var charactersDto = _characterRepository.GetAllCharacters();
            return _characterMapper.MapCharacters(charactersDto);
        }

        public Character GetCharacter(string characterId)
        {
            var characterDto = _characterRepository.GetCharacter(characterId);

            if (characterDto == null)
            {
                return new NullCharacter();
            }

            return _characterMapper.MapSingleCharacter(characterDto);
        }
    }
}
