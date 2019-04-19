﻿using System.Collections.Generic;
using API.Mappers;
using API.Models;
using API.Services;
using Domain.Dtos;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace UnitTests.Services
{
    [TestFixture]
    class CharacterServiceTests
    {
        private Mock<ICharacterRepository> _characterRepositoryMock;
        private CharacterService _characterService;
        private Mock<ICharacterMapper> _characterMapperMock;

        [SetUp]
        public void SetUp()
        {
            _characterRepositoryMock = new Mock<ICharacterRepository>();
            _characterMapperMock = new Mock<ICharacterMapper>();

            _characterService = new CharacterService(_characterRepositoryMock.Object, _characterMapperMock.Object);

            //Setups
            _characterRepositoryMock.Setup(m => m.GetAllCharacters()).Returns(new List<CharacterDto>());
            _characterRepositoryMock.Setup(m => m.GetCharacter(It.IsAny<int>())).Returns(new CharacterDto());
        }

        [Test]
        public void should_call_GetAllCharacters_from_character_repository()
        {
            //given
            //when
            _characterService.GetAllCharacters();

            //then
            _characterRepositoryMock.Verify(m => m.GetAllCharacters());
        }

        [Test]
        public void should_call_GetCharacter_from_character_repository()
        {
            //given
            //when
            _characterService.GetCharacter(It.IsAny<int>());

            //then
            _characterRepositoryMock.Verify(m => m.GetCharacter(It.IsAny<int>()));
        }

        [Test]
        public void should_call_MapCharacters_from_character_mapper()
        {
            //given
            //when
            _characterService.GetAllCharacters();

            //then
            _characterMapperMock.Verify(m => m.MapCharacters(It.IsAny<List<CharacterDto>>()));
        }

        [Test]
        public void should_call_MapSingleCharacter_from_character_mapper()
        {
            //given
            //when
            _characterService.GetCharacter(It.IsAny<int>());

            //then
            _characterMapperMock.Verify(m => m.MapSingleCharacter(It.IsAny<CharacterDto>()));
        }

        [Test]
        public void should_return_list_characters()
        {
            //given
            var charactersDto = new List<CharacterDto>();

            _characterRepositoryMock.Setup(m => m.GetAllCharacters()).Returns(charactersDto);
            _characterMapperMock.Setup(m => m.MapCharacters(charactersDto)).Returns(new List<Character>());

            //when
            var result = _characterService.GetAllCharacters();

            //then
            Assert.IsInstanceOf<List<Character>>(result);
        }

        [Test]
        public void should_return_single_character()
        {
            //given
            var characterId = 1;
            var characterDto = new CharacterDto();

            _characterRepositoryMock.Setup(m => m.GetCharacter(characterId)).Returns(characterDto);
            _characterMapperMock.Setup(m => m.MapSingleCharacter(characterDto)).Returns(new Character());

            //when
            var result = _characterService.GetCharacter(characterId);

            //then
            Assert.IsInstanceOf<Character>(result);
        }

        [Test]
        public void should_return_null_character_when_repository_returns_null()
        {
            //given
            var characterId = 123;

            _characterRepositoryMock.Setup(m => m.GetCharacter(characterId)).Returns((CharacterDto)null);

            //when
            var result = _characterService.GetCharacter(characterId);

            //then
            result.Should().BeOfType<NullCharacter>();
        }
    }
}
