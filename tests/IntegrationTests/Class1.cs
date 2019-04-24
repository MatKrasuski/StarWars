using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using Dapper;
using Domain.DbClients;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    class Class1
    {
        [Test]
        public async Task Name()
        {
            //given
            var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=StarWarsTest;Trusted_Connection=True;");
            var client = new SqlCLient(connection);

            var some = new Dictionary<int, Character>();

            //when
            var result = (await connection.QueryAsync<Character, Episode, Friend, Character>("[Characters].[GetAllCharacters]",
                (character, episode, friend) =>
                {
                    if (!some.TryGetValue(character.CharacterId, out var characterEntry))
                    {
                        some.Add(character.CharacterId, characterEntry = character);
                    }

                    //episodes
                    if (characterEntry.Episodes == null)
                    {
                        characterEntry.Episodes = new List<Episode>();
                    }

                    if (episode != null)
                    {
                        if (characterEntry.Episodes.All(x => x.EpisodeId != episode.EpisodeId))
                        {
                            characterEntry.Episodes.Add(episode);
                        }
                    }

                    //friends
                    if (characterEntry.Friends == null)
                    {
                        characterEntry.Friends = new List<Friend>();
                    }

                    if (friend != null)
                    {
                        if (characterEntry.Friends.All(x => x.FriendId != friend.FriendId))
                        {
                            characterEntry.Friends.Add(friend);
                        }
                    }

                    return characterEntry;
                }, splitOn: "EpisodeId,FriendId"));

            var characters = new List<Character>();

            foreach (var item in some)
            {
                var character = new Character
                {
                    CharacterId = item.Key,
                    Name = item.Value.Name,
                    Planet = item.Value.Planet,
                    Episodes = item.Value.Episodes,
                    Friends = item.Value.Friends
                };

                characters.Add(character);
            }

            //then
        }

        [Test]
        public async Task Name2()
        {
            //given
            var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=StarWarsTest;Trusted_Connection=True;");

            var chars = GetCharacters();

            foreach (var character in chars)
            {
                var characterId = (await connection.QueryAsync<int>("[Characters].[InsertCharacter]",
                    new
                    {
                        character.Name,
                        character.Planet
                    }, 
                    commandType: CommandType.StoredProcedure)).Single();

                foreach (var characterEpisode in character.Episodes)
                {
                    await connection.ExecuteAsync("[Characters].[InsertEpisode]",
                        new
                        {
                            Episode = characterEpisode.EpisodeName,
                            CharacterId = characterId
                        },
                        commandType: CommandType.StoredProcedure);
                }

                foreach (var characterFriend in character.Friends)
                {
                    await connection.ExecuteAsync("[Characters].[InsertFriend]",
                        new
                        {
                            Friend = characterFriend.FriendName,
                            CharacterId = characterId
                        },
                        commandType: CommandType.StoredProcedure);
                }
            }

            //when



            //then
        }

        private static List<Character> GetCharacters()
        {
            return new List<Character>
            {
                new Character
                {
                    Episodes = new List<Episode>
                    {
                        new Episode { EpisodeName = "NEW Episode 1" },
                        new Episode { EpisodeName = "NEW Episode 2" }
                    },
                    Friends = new List<Friend>
                    {
                        new Friend { FriendName = "Friend 1" },
                        new Friend { FriendName = "Friend 1" }
                    },
                    Name = "Name",
                    Planet = "Planet"
                },
                new Character
                {
                    Episodes = new List<Episode>
                    {
                        new Episode { EpisodeName = "NEW Episode 99" },
                        new Episode { EpisodeName = "NEW Episode 89" }
                    },
                    Friends = new List<Friend>
                    {
                        new Friend { FriendName = "Friend 120" },
                        new Friend { FriendName = "Friend 130" }
                    },
                    Name = "LastName",
                    Planet = "Moon"
                }

            };
        }
    }

    //internal class NewCharacter
    //{
    //    public int CharacterId { get; set; }
    //    public string Planet { get; set; }
    //    public string Name { get; set; }
    //    public List<Episode> Episodes { get; set; }
    //    public List<Friend> Friends { get; set; }
    //}

    //internal class Friend
    //{
    //    public int FriendId { get; set; }
    //    public string FriendName { get; set; }
    //}

    //internal class Episode
    //{
    //    public int EpisodeId { get; set; }
    //    public string EpisodeName { get; set; }
    //}
}
