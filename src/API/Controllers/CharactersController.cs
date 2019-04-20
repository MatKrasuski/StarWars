using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.Validation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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

        //[ValidateModel]
        [HttpPost]
        public ActionResult CreateCharacter([FromBody] CharacterBase character)
        {
            if (ModelState.IsValid)
            {
                
            }

            return BadRequest();
        }

        [HttpPatch("{id}")]
        public void Update(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
