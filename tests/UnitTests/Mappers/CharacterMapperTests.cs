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
                },
                new CharacterDto
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
                }
            };

            //when
            var output = _characterMapper.MapCharacters(input);

            output.Should().BeEquivalentTo(input);

            //then
            //Assert.AreEqual(input[0].CharacterId, output[0].Id);
            //Assert.AreEqual(input[0].Episodes.Split(',').ToArray(), output[0].Episodes);
            //Assert.AreEqual(input[0].Friends.Split(',').ToArray(), output[0].Friends);
            //Assert.AreEqual(input[0].Name, output[0].Name);
            //Assert.AreEqual(input[0].Planet, output[0].Planet);

            //Assert.AreEqual(input[1].CharacterId, output[1].Id);
            //Assert.AreEqual(input[1].Episodes.Split(',').ToArray(), output[1].Episodes);
            //Assert.AreEqual(input[1].Friends.Split(',').ToArray(), output[1].Friends);
            //Assert.AreEqual(input[1].Name, output[1].Name);
            //Assert.AreEqual(input[1].Planet, output[1].Planet);

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
            output.Should().BeEquivalentTo(input);

            //Assert.AreEqual(input.CharacterId, output.Id);
            //Assert.AreEqual(input.Episodes.Split(',').ToArray(), output.Episodes);
            //Assert.AreEqual(input.Friends.Split(',').ToArray(), output.Friends);
            //Assert.AreEqual(input.Name, output.Name);
            //Assert.AreEqual(input.Planet, output.Planet);
        }
    }
}
