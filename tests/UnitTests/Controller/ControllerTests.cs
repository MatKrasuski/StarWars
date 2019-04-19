using API.Controllers;
using API.Models;
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
        public void should_call_character_service_from_Get()
        {
            //given
            //when
            _characterController.Get();

            //then
            _charactersServiceMock.Verify(m => m.GetAllCharacters());
        }

        [Test]
        public void should_return_JsonRsult()
        {
            //given
            //when
            var result = _characterController.Get();

            //then
            Assert.IsInstanceOf<JsonResult>(result);
        }
    }
}