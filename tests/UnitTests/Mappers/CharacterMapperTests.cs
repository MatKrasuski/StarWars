using System.Collections.Generic;
using API.Mappers;
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
            var characterDtos = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Id = 1,
                    Episodes = new []{"ep1", "ep2"},
                    Friends = new []{"friend1", "friend2"},
                    Name = "name",
                    Planet = "planet"
                },
                new CharacterDto
                {
                    Id = 2,
                    Episodes = new []{"ep4", "ep5"},
                    Friends = new []{"friend55", "friend222"},
                    Name = "nameName",
                    Planet = "moon"
                }
            };

            //when
            var result = _characterMapper.MapCharacters(characterDtos);

            //then
            result.Should().BeEquivalentTo(characterDtos);
        }

        [Test]
        public void should_map_character_dto_to_character()
        {
            //given
            var characterDto = new CharacterDto
            {
                Id = 2,
                Episodes = new[] {"ep4", "ep5"},
                Friends = new[] {"friend55", "friend222"},
                Name = "nameName",
                Planet = "moon"
            };

            //when
            var result = _characterMapper.MapSingleCharacter(characterDto);

            //then
            result.Should().BeEquivalentTo(characterDto);
        }
    }
}
