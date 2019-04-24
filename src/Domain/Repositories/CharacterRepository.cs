using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using Dapper;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnection _dbConnection;

        public CharacterRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<CharacterDto>> GetAllCharacters()
        {
            return (await _dbConnection.QueryAsync<CharacterDto>("[Characters].[GetCharacters]")).ToList();
        }

        public async Task<CharacterDto> GetCharacter(int characterId)
        {
            return (await _dbConnection.QueryAsync<CharacterDto>("[Characters].[GetCharacterByCharacterId]", 
                new
                {
                    CharacterId = characterId
                }, commandType: CommandType.StoredProcedure)
                ).SingleOrDefault();
        }

        public async Task AddCharacters(List<Character> characters)
        {
            foreach (var character in characters)
            {
                await _dbConnection.ExecuteAsync("[Characters].[InsertCharacters]", 
                    new
                    {
                        Name = character.Name,
                        Episodes = string.Join(',', character.Episodes),
                        character.Planet,
                        Friends = string.Join(',', character.Friends),
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateCharacter(int characterId, Character character)
        {
            await _dbConnection.ExecuteAsync("[Characters].[UpdateCharacter]",
                new
                {
                    CharacterId = characterId,
                    Name = character.Name,
                    Episodes = string.Join(',', character.Episodes),
                    character.Planet,
                    Friends = string.Join(',', character.Friends),
                }, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteCharacter(int characterId)
        {
            await _dbConnection.ExecuteAsync("[Characters].[DeleteCharacter]",
                new
                {
                    CharacterId = characterId
                }, commandType: CommandType.StoredProcedure);
        }
    }
}
