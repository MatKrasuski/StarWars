﻿using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class CharacterService : ICharacterService

    {
        public List<Character> GetAllCharacters()
        {
            return _characters;
        }

        public Character GetCharacter(int id)
        {
            var character = _characters.Where(c => c.Id == id).FirstOrDefault();

            if (character == null)
            {
                return new NullCharacter { Id = id };
            }

            return character;
        }

        private readonly List<Character> _characters = new List<Character>
            {
                new Character
                {
                    Id = 1,
                    Name = "Luke",
                    Episodes = new[]{ "NEWHOPE", "EMPIRE", "JEDI" },
                    Planet = "Tatooine",
                    Friends = new[]{ "Han Solo", "Leia Organa", "C - 3PO", "R2 - D2" }
                },

                new Character
                {
                    Id = 2,
                    Name = "Darth Vader",
                    Episodes = new[]{ "NEWHOPE", "EMPIRE", "JEDI" },
                    Friends = new[]{ "Wilhuff Tarkin" }
                },

                new Character
                {
                    Id = 3,
                    Name = "Han Solo",
                    Episodes = new[]{ "NEWHOPE", "EMPIRE", "JEDI" },
                    Friends = new[]{ "Luke Skywalker", "Leia Organa", "R2-D2" }
                },
            };
    }
}
