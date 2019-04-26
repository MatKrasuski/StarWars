using System.Collections.Generic;
using System.Linq;
using API.Mappers;
using Bussiness.Models;
using Domain.Dtos;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests.Mappers
{
    [TestFixture]
    class CharacterMapperTests
    {
        private CharacterMapper _characterMapper;

        [SetUp]
        public void SetUp()
        {
            _characterMapper = new CharacterMapper();
        }

        [Test]
        public void should_map_list_of_character_dto_to_list_of_character()
        {
            //given
            var input = new List<CharacterDto>
            {
                new CharacterDto
                {
                    CharacterId = 1,
                    Name = "Name",
                    Planet = "Planet",
                    Episodes = new List<Episode>
                    {
                        new Episode {EpisodeName = "NEW Episode 1"},
                        new Episode {EpisodeName = "NEW Episode 2"}
                    },
                    Friends = new List<Friend>
                    {
                        new Friend {FriendName = "Friend 1"},
                        new Friend {FriendName = "Friend 1"}
                    }
                },
                new CharacterDto
                {
                    CharacterId = 2,
                    Name = "LastName",
                    Planet = "Moon",
                    Episodes = new List<Episode>
                    {
                        new Episode {EpisodeName = "NEW Episode 99"},
                        new Episode {EpisodeName = "NEW Episode 89"}
                    },
                    Friends = new List<Friend>
                    {
                        new Friend {FriendName = "Friend 120"},
                        new Friend {FriendName = "Friend 130"}
                    }
                }
            };

            //when
            var output = _characterMapper.MapCharacters(input);

            output[0].CharacterId.Should().Be(input[0].CharacterId);
            output[0].Name.Should().Be(input[0].Name);
            output[0].Planet.Should().Be(input[0].Planet);
            output[0].Episodes.Should().BeEquivalentTo(input[0].Episodes.Select(x => x.EpisodeName).ToArray());
            output[0].Friends.Should().BeEquivalentTo(input[0].Friends.Select(x => x.FriendName).ToArray());

            output[1].CharacterId.Should().Be(input[1].CharacterId);
            output[1].Name.Should().Be(input[1].Name);
            output[1].Planet.Should().Be(input[1].Planet);
            output[1].Episodes.Should().BeEquivalentTo(input[1].Episodes.Select(x => x.EpisodeName).ToArray());
            output[1].Friends.Should().BeEquivalentTo(input[1].Friends.Select(x => x.FriendName).ToArray());

        }

        [Test]
        public void should_map_character_dto_to_character()
        {
            //given
            var input = new CharacterDto
            {
                Episodes = new List<Episode>
                {
                    new Episode {EpisodeName = "NEW Episode 99"},
                    new Episode {EpisodeName = "NEW Episode 89"}
                },
                Friends = new List<Friend>
                {
                    new Friend {FriendName = "Friend 120"},
                    new Friend {FriendName = "Friend 130"}
                },
                Name = "LastName",
                Planet = "Moon"
            };

            //when
            var output = _characterMapper.MapSingleCharacter(input);

            //then
            output.CharacterId.Should().Be(input.CharacterId);
            output.Name.Should().Be(input.Name);
            output.Planet.Should().Be(input.Planet);
            output.Episodes.Should().BeEquivalentTo(input.Episodes.Select(x => x.EpisodeName).ToArray());
            output.Friends.Should().BeEquivalentTo(input.Friends.Select(x => x.FriendName).ToArray());
        }

        [Test]
        public void should_map_characters_to_character_dtos()
        {
            //given
            var input = new List<Character>
            {
                new Character
                {
                    CharacterId = 1,
                    Name = "Name",
                    Planet = "Planet",
                    Episodes = new []{"EP1", "EP2"},
                    Friends = new []{"Fr1", "Fr2"}
                },
                new Character
                {
                    CharacterId = 2,
                    Name = "LastName",
                    Planet = "Moon",
                    Episodes = new []{"EP55", "EP22"},
                    Friends = new []{"Fr123", "Fr456"}
                }
            };

            //when
            var output = _characterMapper.MapCaractersToDtos(input);

            //then
            output[0].CharacterId.Should().Be(input[0].CharacterId);
            output[0].Name.Should().Be(input[0].Name);
            output[0].Planet.Should().Be(input[0].Planet);
            output[0].Episodes.Should().BeEquivalentTo(input[0].Episodes.Select(x => new Episode{EpisodeName = x}).ToList());
            output[0].Friends.Should().BeEquivalentTo(input[0].Friends.Select(x => new Friend { FriendName = x }).ToList());

            output[1].CharacterId.Should().Be(input[1].CharacterId);
            output[1].Name.Should().Be(input[1].Name);
            output[1].Planet.Should().Be(input[1].Planet);
            output[1].Episodes.Should().BeEquivalentTo(input[1].Episodes.Select(x => new Episode{EpisodeName = x}).ToList());
            output[1].Friends.Should().BeEquivalentTo(input[1].Friends.Select(x => new Friend { FriendName = x }).ToList());
        }

        [Test]
        public void should_map_character_to_single_characte_dto()
        {
            //given
            var characterId = 123;
            var input = new Character
            {
                CharacterId = characterId,
                Name = "Name",
                Planet = "Planet",
                Episodes = new[] {"EP1", "EP2"},
                Friends = new[] {"Fr1", "Fr2"}
            };

            //when
            var output = _characterMapper.MapSingleCaracterToDto(characterId, input);

            //then
            output.CharacterId.Should().Be(input.CharacterId);
            output.Name.Should().Be(input.Name);
            output.Planet.Should().Be(input.Planet);
            output.Episodes.Should().BeEquivalentTo(input.Episodes.Select(x => new Episode { EpisodeName = x }).ToList());
            output.Friends.Should().BeEquivalentTo(input.Friends.Select(x => new Friend { FriendName = x }).ToList());
        }
    }
}
