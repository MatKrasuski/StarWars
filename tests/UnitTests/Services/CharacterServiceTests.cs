using System.Collections.Generic;
using System.Threading.Tasks;
using API.Mappers;
using API.Services;
using Bussiness.Models;
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
            _characterRepositoryMock.Setup(m => m.GetAllCharacters()).ReturnsAsync(new List<CharacterDto>());
            _characterRepositoryMock.Setup(m => m.GetCharacter(It.IsAny<string>())).ReturnsAsync(new CharacterDto());
        }

        [Test]
        public async Task should_call_GetAllCharacters_from_character_repository()
        {
            //given
            //when
            await _characterService.GetAllCharacters();

            //then
            _characterRepositoryMock.Verify(m => m.GetAllCharacters());
        }

        [Test]
        public async Task should_call_GetCharacter_from_character_repository()
        {
            //given
            //when
            await _characterService.GetCharacter(It.IsAny<string>());

            //then
            _characterRepositoryMock.Verify(m => m.GetCharacter(It.IsAny<string>()));
        }

        [Test]
        public async Task should_call_MapCharacters_from_character_mapper()
        {
            //given
            //when
            await _characterService.GetAllCharacters();

            //then
            _characterMapperMock.Verify(m => m.MapCharacters(It.IsAny<List<CharacterDto>>()));
        }

        [Test]
        public async Task should_call_MapSingleCharacter_from_character_mapper()
        {
            //given
            //when
            await _characterService.GetCharacter(It.IsAny<string>());

            //then
            _characterMapperMock.Verify(m => m.MapSingleCharacter(It.IsAny<CharacterDto>()));
        }

        [Test]
        public async Task should_return_list_characters()
        {
            //given
            var charactersDto = new List<CharacterDto>();

            _characterRepositoryMock.Setup(m => m.GetAllCharacters()).ReturnsAsync(charactersDto);
            _characterMapperMock.Setup(m => m.MapCharacters(charactersDto)).Returns(new List<Character>());

            //when
            var result = await _characterService.GetAllCharacters();

            //then
            Assert.IsInstanceOf<List<Character>>(result);
        }

        [Test]
        public async Task should_return_single_character()
        {
            //given
            var characterId = "123";
            var characterDto = new CharacterDto();

            _characterRepositoryMock.Setup(m => m.GetCharacter(characterId)).ReturnsAsync(characterDto);
            _characterMapperMock.Setup(m => m.MapSingleCharacter(characterDto)).Returns(new Character());

            //when
            var result = await _characterService.GetCharacter(characterId);

            //then
            Assert.IsInstanceOf<Character>(result);
        }

        [Test]
        public async Task should_return_null_character_when_repository_returns_null()
        {
            //given
            var characterId = "123";

            _characterRepositoryMock.Setup(m => m.GetCharacter(characterId)).ReturnsAsync((CharacterDto)null);

            //when
            var result = await _characterService.GetCharacter(characterId);

            //then
            result.Should().BeOfType<NullCharacter>();
        }

        [Test]
        public async Task should_call_character_repository_AddCharacter()
        {
            //given
            var character = new List<CharacterBase>{new CharacterBase()};

            //when
            await _characterService.AddCharacters(character);

            //then
            _characterRepositoryMock.Verify(m => m.AddCharacters(character));
        }

        [Test]
        public async Task should_call_character_repository_UpdateCharacter()
        {
            //given
            var id = "123";
            var character = new CharacterBase();

            //when
            await _characterService.UpdateCharacter(id, character);

            //then
            _characterRepositoryMock.Verify(m => m.UpdateCharacter(id, character));
        }

        [Test]
        public async Task should_call_character_repository_DeleteCharacter()
        {
            //given
            var id = "123";

            //when
            await _characterService.DeleteCharacter(id);

            //then
            _characterRepositoryMock.Verify(m => m.DeleteCharacter(id));
        }
    }
}
