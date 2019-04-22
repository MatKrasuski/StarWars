using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using Domain.Dtos;
using Domain.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.Repositories
{
    [TestFixture]
    class CharacterRepositoryTests : TestBase
    {
        private CharacterRepository _characterRepository;

        [OneTimeSetUp]
        public async Task SetupOnce()
        {
            await ClearCharactersTable();

            _characterRepository = new CharacterRepository(SqlClient);
        }

        [Test]
        public async Task should_get_all_characters()
        {
            //given
            await ClearCharactersTable();

            var character1 = new CharacterDto
            {
                CharacterId = 1,
                Episodes = "abc, gfg",
                Planet = "planet",
                Name = "Luke",
                Friends = "f1,f2"
            };

            var character2 = new CharacterDto
            {
                CharacterId = 1,
                Episodes = "qqq, bbb",
                Name = "Lukas",
                Friends = "q1, q2"
            };

            var characters = new List<CharacterDto>
            {
                character1,
                character2
            };

            await InsertCharacterDto(character1);
            await InsertCharacterDto(character2);

            //when
            var result = await _characterRepository.GetAllCharacters();

            //then
            result.Should().ContainEquivalentOf(characters[0]);
        }

        [Test]
        public async Task should_get_single_character()
        {
            //given
            var character = new CharacterDto
            {
                Episodes = "abc, gfg",
                Planet = "planet",
                Name = "Luke",
                Friends = "f1,f2"
            };

            var insertedCandidateId = await InsertCharacterDto(character);

            //when
            var result = await _characterRepository.GetCharacter(insertedCandidateId);

            //then
            result.Should().BeEquivalentTo(character, o => o.Excluding(s => s.CharacterId));
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

            var inserted1 = await InsertCharacter(characters[0]);
            var inserted2 = await InsertCharacter(characters[1]);

            //when
            await _characterRepository.AddCharacters(characters);

            //then
            var result1 = await _characterRepository.GetCharacter(inserted1);
            var result2 = await _characterRepository.GetCharacter(inserted2);

            result1.Name.Should().Be(characters[0].Name);
            result1.Episodes.Should().Be(string.Join(',', characters[0].Episodes));
            result1.Friends.Should().Be(string.Join(',', characters[0].Friends));
            result1.Planet.Should().Be(characters[0].Planet);

            result2.Name.Should().Be(characters[1].Name);
            result2.Episodes.Should().Be(string.Join(',', characters[1].Episodes));
            result2.Friends.Should().Be(string.Join(',', characters[1].Friends));
            result2.Planet.Should().Be(characters[1].Planet);
        }

        [Test]
        public async Task should_Update_character()
        {
            //given
            var character = new CharacterBase
            {
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

            var characterInsertedId = await InsertCharacter(character);

            //when
            await _characterRepository.UpdateCharacter(characterInsertedId, characterToUpdate);

            //then
            var result = await _characterRepository.GetCharacter(characterInsertedId);

            result.Friends.Should().Be(string.Join(',', characterToUpdate.Friends));
            result.Episodes.Should().Be(string.Join(',', characterToUpdate.Episodes));
            result.Name.Should().Be(characterToUpdate.Name);
            result.Planet.Should().Be(characterToUpdate.Planet);
        }

        [Test]
        public async Task should_Delete_character()
        {
            //given
            var character = new CharacterBase
            {
                Episodes = new[] { "abc", "gfg" },
                Planet = "planet",
                Name = "Luke",
                Friends = new[] { "f1", "f2" }
            };

            var insertedCharacterId = await InsertCharacter(character);

            //when
            await _characterRepository.DeleteCharacter(insertedCharacterId);

            //then
            var result = await _characterRepository.GetCharacter(insertedCharacterId);

            result.Should().BeNull();
        }

        private async Task<int> InsertCharacterDto(CharacterDto character)
        {
            return (await SqlClient.QueryAsync<int>("insert into [Characters].[StarWarsCharacters] (Name, Episodes, Planet, Friends)" +
                                                    $"Values('{character.Name}', '{character.Episodes}', '{character.Planet}', '{character.Friends}')" +
                                                    $"SELECT CAST(SCOPE_IDENTITY() as int)",
                null, CommandType.Text)).Single();
        }

        private async Task<int> InsertCharacter(Character character)
        {
            return (await SqlClient.QueryAsync<int>("insert into [Characters].[StarWarsCharacters] (Name, Episodes, Planet, Friends)" +
                                                    $"Values('{character.Name}', '{string.Join(',',character.Episodes)}', '{character.Planet}', '{string.Join(',', character.Friends)}')" +
                                                    $"SELECT CAST(SCOPE_IDENTITY() as int)",
                null, CommandType.Text)).Single();
        }
    }
}
