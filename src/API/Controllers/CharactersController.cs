using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services;
using API.Validation;
using Bussiness.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return new JsonResult(await _characterService.GetAllCharacters());
        }

        [ValidateIdFormat]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var character = await _characterService.GetCharacter(id);

            if (character is NullCharacter)
            {
                return NoContent();
            }

            return new JsonResult(character);
        }

        [HttpPost]
        public async Task Add([FromBody] List<Character> characters)
        {
            await _characterService.AddCharacters(characters);
        }

        [ValidateIdFormat]
        [HttpPut("{id}")]
        public async Task Update(string id, [FromBody] Character character)
        {
            await _characterService.UpdateCharacter(id, character);
        }

        [ValidateIdFormat]
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _characterService.DeleteCharacter(id);
        }
    }
}
