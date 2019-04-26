using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using Dapper;
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

            _characterRepository = new CharacterRepository(DbConnection);
        }

        [Test]
        public async Task should_get_all_characters()
        {
            //given
            var characters = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Episodes = new List<Episode>
                    {
                        new Episode { EpisodeName = "NEW Episode 1" },
                        new Episode { EpisodeName = "NEW Episode 2" }
                    },
                    Friends = new List<Friend>
                    {
                        new Friend { FriendName = "Friend 1" },
                        new Friend { FriendName = "Friend 1" }
                    },
                    Name = "Name",
                    Planet = "Planet"
                },
                new CharacterDto
                {
                    Episodes = new List<Episode>
                    {
                        new Episode { EpisodeName = "NEW Episode 99" },
                        new Episode { EpisodeName = "NEW Episode 89" }
                    },
                    Friends = new List<Friend>
                    {
                        new Friend { FriendName = "Friend 120" },
                        new Friend { FriendName = "Friend 130" }
                    },
                    Name = "LastName",
                    Planet = "Moon"
                }

            };

            await InsertCharacter(characters[0]);
            await InsertCharacter(characters[1]);

            //when
            var result = await _characterRepository.GetAllCharacters();

            //then
            result[0].Name.Should().Be(characters[0].Name);
            result[0].Planet.Should().Be(characters[0].Planet);
            result[0].Episodes[0].EpisodeName.Should().Be(characters[0].Episodes[0].EpisodeName);
            result[0].Episodes[1].EpisodeName.Should().Be(characters[0].Episodes[1].EpisodeName);
            result[0].Friends[0].FriendName.Should().Be(characters[0].Friends[0].FriendName);
            result[0].Friends[1].FriendName.Should().Be(characters[0].Friends[1].FriendName);

            result[1].Name.Should().Be(characters[1].Name);
            result[1].Planet.Should().Be(characters[1].Planet);
            result[1].Episodes[0].EpisodeName.Should().Be(characters[1].Episodes[0].EpisodeName);
            result[1].Episodes[1].EpisodeName.Should().Be(characters[1].Episodes[1].EpisodeName);
            result[1].Friends[0].FriendName.Should().Be(characters[1].Friends[0].FriendName);
            result[1].Friends[1].FriendName.Should().Be(characters[1].Friends[1].FriendName);
        }

        [Test]
        public async Task should_get_single_character()
        {
            //given
            var character = new CharacterDto
            {
                Episodes = new List<Episode>
                {
                    new Episode {EpisodeName = "NEW Episode 1"},
                    new Episode {EpisodeName = "NEW Episode 2"}
                },
                Friends = new List<Friend>
                {
                    new Friend {FriendName = "Friend 1"},
                    new Friend {FriendName = "Friend 1"}
                },
                Name = "Name",
                Planet = "Planet"
            };

            var insertedCandidateId = await InsertCharacter(character);

            //when
            var result = await _characterRepository.GetCharacter(insertedCandidateId);

            //then
            result.Name.Should().Be(character.Name);
            result.Planet.Should().Be(character.Planet);
            result.Episodes[0].EpisodeName.Should().Be(character.Episodes[0].EpisodeName);
            result.Episodes[1].EpisodeName.Should().Be(character.Episodes[1].EpisodeName);
            result.Friends[0].FriendName.Should().Be(character.Friends[0].FriendName);
            result.Friends[1].FriendName.Should().Be(character.Friends[1].FriendName);
        }

        [Test]
        public async Task should_add_characters()
        {
            //given
            await ClearCharactersTable();

            var characters = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Episodes = new List<Episode>
                    {
                        new Episode { EpisodeName = "NEW Episode 1" },
                        new Episode { EpisodeName = "NEW Episode 2" }
                    },
                    Friends = new List<Friend>
                    {
                        new Friend { FriendName = "Friend 1" },
                        new Friend { FriendName = "Friend 1" }
                    },
                    Name = "Name",
                    Planet = "Planet"
                },
                new CharacterDto
                {
                    Episodes = new List<Episode>
                    {
                        new Episode { EpisodeName = "NEW Episode 99" },
                        new Episode { EpisodeName = "NEW Episode 89" }
                    },
                    Friends = new List<Friend>
                    {
                        new Friend { FriendName = "Friend 120" },
                        new Friend { FriendName = "Friend 130" }
                    },
                    Name = "LastName",
                    Planet = "Moon"
                }
            };

            //when
            await _characterRepository.AddCharacters(characters);

            //then
            var result = await _characterRepository.GetAllCharacters();

            //then
            result[0].Name.Should().Be(characters[0].Name);
            result[0].Planet.Should().Be(characters[0].Planet);
            result[0].Episodes[0].EpisodeName.Should().Be(characters[0].Episodes[0].EpisodeName);
            result[0].Episodes[1].EpisodeName.Should().Be(characters[0].Episodes[1].EpisodeName);
            result[0].Friends[0].FriendName.Should().Be(characters[0].Friends[0].FriendName);
            result[0].Friends[1].FriendName.Should().Be(characters[0].Friends[1].FriendName);

            result[1].Name.Should().Be(characters[1].Name);
            result[1].Planet.Should().Be(characters[1].Planet);
            result[1].Episodes[0].EpisodeName.Should().Be(characters[1].Episodes[0].EpisodeName);
            result[1].Episodes[1].EpisodeName.Should().Be(characters[1].Episodes[1].EpisodeName);
            result[1].Friends[0].FriendName.Should().Be(characters[1].Friends[0].FriendName);
            result[1].Friends[1].FriendName.Should().Be(characters[1].Friends[1].FriendName);
        }

        [Test]
        public async Task should_Update_character()
        {
            //given
            var character = new CharacterDto
            {
                Episodes = new List<Episode>
                {
                    new Episode {EpisodeName = "NEW Episode 1"},
                    new Episode {EpisodeName = "NEW Episode 2"}
                },
                Friends = new List<Friend>
                {
                    new Friend {FriendName = "Friend 1"},
                    new Friend {FriendName = "Friend 1"}
                },
                Name = "Name",
                Planet = "Planet"
            };

            var characterToUpdate = new CharacterDto
            {
                Episodes = new List<Episode>
                {
                    new Episode {EpisodeName = "NEWHOPE"},
                    new Episode {EpisodeName = "EMPIRE"}
                },
                Friends = new List<Friend>
                {
                    new Friend {FriendName = "Han Solo"},
                    new Friend {FriendName = "Leia Organa"}
                },
                Name = "Luke",
                Planet = "Planet"
            };

            var characterInsertedId = await InsertCharacter(character);

            //when
            await _characterRepository.UpdateCharacter(characterToUpdate);

            //then
            var result = await _characterRepository.GetCharacter(characterInsertedId);

            result.Name.Should().Be(characterToUpdate.Name);
            result.Planet.Should().Be(characterToUpdate.Planet);
            result.Episodes[0].EpisodeName.Should().Be(characterToUpdate.Episodes[0].EpisodeName);
            result.Episodes[1].EpisodeName.Should().Be(characterToUpdate.Episodes[1].EpisodeName);
            result.Friends[0].FriendName.Should().Be(characterToUpdate.Friends[0].FriendName);
            result.Friends[1].FriendName.Should().Be(characterToUpdate.Friends[1].FriendName);
        }

        [Test]
        public async Task should_Delete_character()
        {
            //given
            var character = new CharacterDto
            {
                Episodes = new List<Episode>
                {
                    new Episode {EpisodeName = "NEW Episode 1"},
                    new Episode {EpisodeName = "NEW Episode 2"}
                },
                Friends = new List<Friend>
                {
                    new Friend {FriendName = "Friend 1"},
                    new Friend {FriendName = "Friend 1"}
                },
                Name = "Name",
                Planet = "Planet"
            };

            var insertedCharacterId = await InsertCharacter(character);

            //when
            await _characterRepository.DeleteCharacter(insertedCharacterId);

            //then
            var result = await _characterRepository.GetCharacter(insertedCharacterId);

            result.Should().BeNull();
        }

        private async Task<int> InsertCharacter(CharacterDto character)
        {
            var characterId = (await DbConnection.QueryAsync<int>("[Characters].[InsertCharacter]",
                new
                {
                    character.Name,
                    character.Planet
                },
                commandType: CommandType.StoredProcedure)).Single();

            foreach (var characterEpisode in character.Episodes)
            {
                await DbConnection.ExecuteAsync("[Characters].[InsertEpisode]",
                    new
                    {
                        Episode = characterEpisode.EpisodeName,
                        CharacterId = characterId
                    },
                    commandType: CommandType.StoredProcedure);
            }

            foreach (var characterFriend in character.Friends)
            {
                await DbConnection.ExecuteAsync("[Characters].[InsertFriend]",
                    new
                    {
                        Friend = characterFriend.FriendName,
                        CharacterId = characterId
                    },
                    commandType: CommandType.StoredProcedure);
            }

            return characterId;
        }
    }
}
