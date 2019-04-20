using System.Collections.Generic;
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

        public List<CharacterDto> GetAllCharacters()
        {
            var collection = _database.GetCollection<CharacterDto>(_collection);
            return collection.Find(FilterDefinition<CharacterDto>.Empty).ToList();
        }

        public CharacterDto GetCharacter(string characterId)
        {
            var collection = _database.GetCollection<CharacterDto>(_collection);
            return collection.Find(Builders<CharacterDto>.Filter.Eq("_id", ObjectId.Parse(characterId))).FirstOrDefault();
        }
    }
}
