using API.Models;
using System.Collections.Generic;
using MongoDB.Bson;

namespace API.Services
{
    public interface ICharacterService
    {
        List<Character> GetAllCharacters();
        Character GetCharacter(string characterId);
    }
}