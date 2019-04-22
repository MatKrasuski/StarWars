using System.Collections.Generic;
using System.Linq;
using API.Mappers;
using Domain.Dtos;
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
                    Episodes = "ep1, ep2",
                    Friends = "friend1, friend2",
                    CharacterName = "name",
                    Planet = "planet"
                },
                new CharacterDto
                {
                    CharacterId =2,
                    Episodes = "ep4, ep5",
                    Friends = "friend55, friend222",
                    CharacterName = "nameName"
                }
            };

            //when
            var output = _characterMapper.MapCharacters(input);

            //then
            Assert.AreEqual(input[0].CharacterId, output[0].Id);
            Assert.AreEqual(input[0].Episodes.Split(',').ToArray(), output[0].Episodes);
            Assert.AreEqual(input[0].Friends.Split(',').ToArray(), output[0].Friends);
            Assert.AreEqual(input[0].CharacterName, output[0].CharacterName);
            Assert.AreEqual(input[0].Planet, output[0].Planet);

            Assert.AreEqual(input[1].CharacterId, output[1].Id);
            Assert.AreEqual(input[1].Episodes.Split(',').ToArray(), output[1].Episodes);
            Assert.AreEqual(input[1].Friends.Split(',').ToArray(), output[1].Friends);
            Assert.AreEqual(input[1].CharacterName, output[1].CharacterName);
            Assert.AreEqual(input[1].Planet, output[1].Planet);

        }

        [Test]
        public void should_map_character_dto_to_character()
        {
            //given
            var input = new CharacterDto
            {
                CharacterId = 1,
                Episodes = "ep4, ep5" ,
                Friends = "friend55, friend222",
                CharacterName = "nameName",
                Planet = "moon"
            };

            //when
            var output = _characterMapper.MapSingleCharacter(input);

            //then
            Assert.AreEqual(input.CharacterId, output.Id);
            Assert.AreEqual(input.Episodes.Split(',').ToArray(), output.Episodes);
            Assert.AreEqual(input.Friends.Split(',').ToArray(), output.Friends);
            Assert.AreEqual(input.CharacterName, output.CharacterName);
            Assert.AreEqual(input.Planet, output.Planet);
        }
    }
}
