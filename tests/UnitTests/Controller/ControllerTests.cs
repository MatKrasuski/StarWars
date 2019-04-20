using API.Controllers;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

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
            _charactersServiceMock.Setup(m => m.GetAllCharacters()).Returns(new List<Character>());
        }

        [Test]
        public void should_call_GetAllCharacters_from_character_service()
        {
            //given
            //when
            var result = _characterController.Get();

            //then
            _charactersServiceMock.Verify(m => m.GetAllCharacters());
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void should_call_GetCharacter_from_character_service()
        {
            //given
            var id = "123";
            //when
            var result = _characterController.Get(id);

            //then
            _charactersServiceMock.Verify(m => m.GetCharacter(id));
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void should_return_no_content_if_service_returns_NullCandidate()
        {
            //given
            var id = "123";

            _charactersServiceMock.Setup(m => m.GetCharacter(id)).Returns(new NullCharacter());

            //when
            var result = _characterController.Get(id);

            //then
            Assert.IsInstanceOf<NoContentResult>(result);
        }

    }
}