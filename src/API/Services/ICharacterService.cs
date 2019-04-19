using API.Models;
using System.Collections.Generic;

namespace API.Services
{
    public interface ICharacterService
    {
        List<Character> GetAllCharacters();
        Character GetCharacter(int id);
    }
}