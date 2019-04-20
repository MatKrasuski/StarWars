using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;
using Domain.Dtos;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly string _dbName = "StarWars";
        private readonly string _collection = "characters";

        public CharacterRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _database = _mongoClient.GetDatabase(_dbName);
        }

        //Constructor for integration tests
        public CharacterRepository(IMongoClient mongoClient, string database, string collectionName) : this(mongoClient)
        {
            _database = _mongoClient.GetDatabase(database);
            _collection = collectionName;
        }

        public async Task<List<CharacterDto>> GetAllCharacters()
        {
            var collection = _database.GetCollection<CharacterDto>(_collection);
            return (await collection.FindAsync(FilterDefinition<CharacterDto>.Empty)).ToList();
        }

        public async Task<CharacterDto> GetCharacter(string characterId)
        {
            var collection = _database.GetCollection<CharacterDto>(_collection);
            return (await collection.FindAsync(Builders<CharacterDto>.Filter.Eq("_id", ObjectId.Parse(characterId)))).FirstOrDefault();
        }

        public async Task AddCharacter(CharacterBase character)
        {
            var collection = _database.GetCollection<CharacterBase>(_collection);
            await collection.InsertOneAsync(character);
        }
    }
}
