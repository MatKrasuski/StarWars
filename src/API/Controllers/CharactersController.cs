using API.Models;
using API.Services;
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
        public ActionResult Get()
        {
            return new JsonResult(_characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var character = _characterService.GetCharacter(id);

            if (character is NullCharacter)
            {
                return NoContent();
            }

            return new JsonResult(character);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
