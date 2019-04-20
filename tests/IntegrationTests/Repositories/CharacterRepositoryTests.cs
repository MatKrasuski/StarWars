using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;
using Domain.Dtos;
using Domain.Repositories;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;

namespace IntegrationTests.Repositories
{
    [TestFixture]
    class CharacterRepositoryTests : TestBase
    {
        private CharacterRepository _characterRepository;
        private const string CharactersCollection = "charactersTest";

        [OneTimeSetUp]
        public void SetupOnce()
        {
            Db.DropCollection(CharactersCollection);

            _characterRepository = new CharacterRepository(MongoClient, DbName, CharactersCollection);
        }

        [Test]
        public async Task should_get_all_characters()
        {
            //given
            var characters = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Id = ObjectId.GenerateNewId(),
                    Episodes = new[] {"abc", "gfg"},
                    Planet = "planet",
                    Name = "Luke",
                    Friends = new []{"f1", "f2"}
                },
                new CharacterDto
                {
                    Id = ObjectId.GenerateNewId(),
                    Episodes = new[] {"qqq", "bbb"},
                    Name = "Lukas",
                    Friends = new []{"q1", "q2"}
                }
            };

            var collection = Db.GetCollection<CharacterDto>(CharactersCollection);
            collection.InsertMany(characters);

            //when
            var result = await _characterRepository.GetAllCharacters();

            //then
            result.Should().BeEquivalentTo(characters);
        }

        [Test]
        public async Task should_get_single_character()
        {
            //given
            var characterId = ObjectId.GenerateNewId();
            var character = new CharacterDto
            {
                Id = characterId,
                Episodes = new[] {"abc", "gfg"},
                Planet = "planet",
                Name = "Luke",
                Friends = new[] {"f1", "f2"}
            };

            var collection = Db.GetCollection<CharacterDto>(CharactersCollection);
            collection.InsertOne(character);

            //when
            var result = await _characterRepository.GetCharacter(characterId.ToString());

            //then
            result.Should().BeEquivalentTo(character);
        }

        [Test]
        public async Task should_add_character()
        {
            //given
            var character = new CharacterBase
            {
                Episodes = new[] { "abc", "gfg" },
                Planet = "planet",
                Name = "Luke",
                Friends = new[] { "f1", "f2" }
            };

            //when
            await _characterRepository.AddCharacter(character);

            //then
            var result = await _characterRepository.GetAllCharacters();

            result.Should().ContainEquivalentOf(character);
        }
    }
}
