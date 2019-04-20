using API.Controllers;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bussiness.Models;

namespace UnitTests.Controller
{
    public class CharactersControllerTests
    {
        private Mock<ICharacterService> _charactersServiceMock;
        private CharactersController _characterController;

        [SetUp]
        public void Setup()
        {
            _charactersServiceMock = new Mock<ICharacterService>();

            _characterController = new CharactersController(_charactersServiceMock.Object);

            // Setups
            _charactersServiceMock.Setup(m => m.GetAllCharacters()).ReturnsAsync(new List<Character>());
        }

        [Test]
        public async Task should_call_GetAllCharacters_from_character_service()
        {
            //given
            //when
            var result = await _characterController.Get();

            //then
            _charactersServiceMock.Verify(m => m.GetAllCharacters());
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public async Task should_call_GetCharacter_from_character_service()
        {
            //given
            var id = "123";
            //when
            var result = await _characterController.Get(id);

            //then
            _charactersServiceMock.Verify(m => m.GetCharacter(id));
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public async Task should_return_no_content_if_service_returns_NullCandidate()
        {
            //given
            var id = "123";

            _charactersServiceMock.Setup(m => m.GetCharacter(id)).ReturnsAsync(new NullCharacter());

            //when
            var result = await _characterController.Get(id);

            //then
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task should_call_CreateCharacter_from_service()
        {
            //given
            var character = new CharacterBase
            {
                Name = "luke",
                Planet = "Tatooine",
                Episodes = new []{"ep 1", "ep 2"},
                Friends = new[] {"friend 1", "friend 2"}
            };

            //when
            await _characterController.AddCharacter(character);

            //then
            _charactersServiceMock.Verify(m => m.AddCharacter(character));
        }
    }
}