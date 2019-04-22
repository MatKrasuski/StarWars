using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using Domain.DbClients;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ISqlClient _sqlClient;

        public CharacterRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public async Task<List<CharacterDto>> GetAllCharacters()
        {
            return (await _sqlClient.QueryAsync<CharacterDto>("[Characters].[GetCharacters]")).ToList();
        }

        public async Task<CharacterDto> GetCharacter(int characterId)
        {
            return (await _sqlClient.QueryAsync<CharacterDto>("[Characters].[GetCharacterByCharacterId]", 
                new
                {
                    CharacterId = characterId
                })
                ).FirstOrDefault();
        }

        public async Task AddCharacters(List<Character> characters)
        {
            foreach (var character in characters)
            {
                await _sqlClient.ExecuteAsync("[Characters].[InsertCharacters]", 
                    new
                    {
                        Name = character.Name,
                        Episodes = string.Join(',', character.Episodes),
                        character.Planet,
                        Friends = string.Join(',', character.Friends),
                    });
            }
        }

        public async Task UpdateCharacter(int characterId, Character character)
        {
            await _sqlClient.ExecuteAsync("[Characters].[UpdateCharacter]",
                new
                {
                    CharacterId = characterId,
                    Name = character.Name,
                    Episodes = string.Join(',', character.Episodes),
                    character.Planet,
                    Friends = string.Join(',', character.Friends),
                });
        }

        public async Task DeleteCharacter(int characterId)
        {
            await _sqlClient.ExecuteAsync("[Characters].[DeleteCharacter]",
                new
                {
                    CharacterId = characterId
                });
        }
    }
}
