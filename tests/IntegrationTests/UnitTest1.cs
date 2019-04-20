using System;
using System.Collections.Generic;
using Domain.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private IMongoDatabase _database;

        [SetUp]
        public void Setup()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("StarWars");
        }

        [Test]
        public void Test1()
        {
            var model = new List<CharacterDto>
            {
                new CharacterDto
                {
                    Id = ObjectId.GenerateNewId(DateTime.Now),
                    Episodes = new[] {"abc", "gfg"},
                    Planet = "planet",
                    Name = "Luke",
                    Friends = new []{"f1", "f2"}
                },
                new CharacterDto
                {
                    Id = ObjectId.GenerateNewId(DateTime.Now),
                    Episodes = new[] {"qqq", "bbb"},
                    Name = "Lukas",
                    Friends = new []{"q1", "q2"}
                }
            };

            var collection = _database.GetCollection<CharacterDto>("characters");

            collection.InsertMany(model);
        }

        [Test]
        public void Ttt()
        {
            //given

            //when
            var collection = _database.GetCollection<CharacterDto>("characters");

            var result = collection.Find(FilterDefinition<CharacterDto>.Empty).ToList();


            //then
        }
    }
}