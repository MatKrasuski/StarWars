using System.Collections.Generic;
using API.Mappers;
using Domain.Dtos;
using MongoDB.Bson;
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
                    Id = ObjectId.GenerateNewId(),
                    Episodes = new []{"ep1", "ep2"},
                    Friends = new []{"friend1", "friend2"},
                    Name = "name",
                    Planet = "planet"
                },
                new CharacterDto
                {
                    Id = ObjectId.GenerateNewId(),
                    Episodes = new []{"ep4", "ep5"},
                    Friends = new []{"friend55", "friend222"},
                    Name = "nameName"
                }
            };

            //when
            var output = _characterMapper.MapCharacters(input);

            //then
            Assert.AreEqual(input[0].Id.ToString(), output[0].Id);
            Assert.AreEqual(input[0].Episodes, output[0].Episodes);
            Assert.AreEqual(input[0].Friends, output[0].Friends);
            Assert.AreEqual(input[0].Name, output[0].Name);
            Assert.AreEqual(input[0].Planet, output[0].Planet);

            Assert.AreEqual(input[1].Id.ToString(), output[1].Id);
            Assert.AreEqual(input[1].Episodes, output[1].Episodes);
            Assert.AreEqual(input[1].Friends, output[1].Friends);
            Assert.AreEqual(input[1].Name, output[1].Name);
            Assert.AreEqual(input[1].Planet, output[1].Planet);

        }

        [Test]
        public void should_map_character_dto_to_character()
        {
            //given
            var input = new CharacterDto
            {
                Id = ObjectId.GenerateNewId(),
                Episodes = new[] {"ep4", "ep5"},
                Friends = new[] {"friend55", "friend222"},
                Name = "nameName",
                Planet = "moon"
            };

            //when
            var output = _characterMapper.MapSingleCharacter(input);

            //then
            Assert.AreEqual(input.Id.ToString(), output.Id);
            Assert.AreEqual(input.Episodes, output.Episodes);
            Assert.AreEqual(input.Friends, output.Friends);
            Assert.AreEqual(input.Name, output.Name);
            Assert.AreEqual(input.Planet, output.Planet);
        }
    }
}
