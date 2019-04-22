using System;
using System.Collections.Generic;
using System.Linq;
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
                Episodes = new[] { "abc", "gfg" },
                Planet = "planet",
                Name = "Luke",
                Friends = new[] { "f1", "f2" }
            };

            var collection = Db.GetCollection<CharacterDto>(CharactersCollection);
            collection.InsertOne(character);

            //when
            var result = await _characterRepository.GetCharacter(characterId.ToString());

            //then
            result.Should().BeEquivalentTo(character);
        }

        [Test]
        public async Task should_add_characters()
        {
            //given
            var characters = new List<Character>
            {
                new Character
                {
                    Episodes = new[] {"abc", "gfg"},
                    Planet = "planet",
                    Name = "Luke",
                    Friends = new[] {"f1", "f2"}
                },

                new Character
                {
                    Episodes = new[] {"nnn", "www"},
                    Planet = "moon",
                    Name = "Vader",
                    Friends = new[] {"f0"}
                }
            };

            //when
            await _characterRepository.AddCharacters(characters);

            //then
            var result = await _characterRepository.GetAllCharacters();

            result.Should().ContainEquivalentOf(characters[0]);
            result.Should().ContainEquivalentOf(characters[1]);
        }

        [Test]
        public async Task should_Update_character()
        {
            //given
            var id = ObjectId.GenerateNewId().ToString();

            var character = new CharacterBase
            {
                Id = id,
                Episodes = new[] { "abc", "gfg" },
                Planet = "planet",
                Name = "Luke",
                Friends = new[] { "f1", "f2" }
            };

            var characterToUpdate = new Character
            {
                Episodes = new[] { "NEWHOPE", "EMPIRE", "JEDI" },
                Name = "Luke",
                Friends = new[] { "Han Solo", "Leia Organa", "C-3PO", "R2-D2" }
            };

            var collection = Db.GetCollection<CharacterBase>(CharactersCollection);
            collection.InsertOne(character);

            //when
            await _characterRepository.UpdateCharacter(id, characterToUpdate);

            //then
            var result = await _characterRepository.GetCharacter(id);

            result.Should().BeEquivalentTo(characterToUpdate);
        }

        [Test]
        public async Task should_Delete_character()
        {
            //given
            var id = ObjectId.GenerateNewId().ToString();

            var character = new CharacterBase
            {
                Id = id,
                Episodes = new[] { "abc", "gfg" },
                Planet = "planet",
                Name = "Luke",
                Friends = new[] { "f1", "f2" }
            };

            var collection = Db.GetCollection<CharacterBase>(CharactersCollection);
            collection.InsertOne(character);

            //when
            await _characterRepository.DeleteCharacter(id);

            //then
            var result = await _characterRepository.GetCharacter(id);

            result.Should().BeNull();
        }
    }
}
