using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using Dapper;
using Domain.Dtos;
using Domain.Interfaces;

namespace Domain.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnection _dbConnection;

        public CharacterRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<CharacterDto>> GetAllCharacters()
        {
            var charactersDictionary = new Dictionary<int, CharacterDto>();

            //when
            await _dbConnection.QueryAsync<CharacterDto, Episode, Friend, CharacterDto>("[Characters].[GetAllCharacters]",
                (character, episode, friend) =>
                {
                    if (!charactersDictionary.TryGetValue(character.CharacterId, out var characterEntry))
                    {
                        charactersDictionary.Add(character.CharacterId, characterEntry = character);
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

                }, splitOn: "EpisodeId,FriendId",
                commandType: CommandType.StoredProcedure);

            var characters = new List<CharacterDto>();

            foreach (var item in charactersDictionary)
            {
                var character = new CharacterDto
                {
                    CharacterId = item.Key,
                    Name = item.Value.Name,
                    Planet = item.Value.Planet,
                    Episodes = item.Value.Episodes,
                    Friends = item.Value.Friends
                };

                characters.Add(character);
            }

            return characters;
        }

        public async Task<CharacterDto> GetCharacter(int characterId)
        {
            var charactersDictionary = new Dictionary<int, CharacterDto>();

            //when
            await _dbConnection.QueryAsync<CharacterDto, Episode, Friend, CharacterDto>("[Characters].[GetCharacterById]",
                (character, episode, friend) =>
                {
                    if (!charactersDictionary.TryGetValue(character.CharacterId, out var characterEntry))
                    {
                        charactersDictionary.Add(character.CharacterId, characterEntry = character);
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

                },
                new { CharacterId = characterId },
                commandType: CommandType.StoredProcedure,
                splitOn: "EpisodeId,FriendId");

            var characters = new List<CharacterDto>();

            foreach (var item in charactersDictionary)
            {
                var character = new CharacterDto
                {
                    CharacterId = item.Key,
                    Name = item.Value.Name,
                    Planet = item.Value.Planet,
                    Episodes = item.Value.Episodes,
                    Friends = item.Value.Friends
                };

                characters.Add(character);
            }

            return characters.Single();
        }

        public async Task AddCharacters(List<Character> characters)
        {
            foreach (var character in characters)
            {
                var friends = new DataTable();
                friends.Columns.Add("StringItem", typeof(string));

                foreach (var characterFriend in characters.SelectMany(s => s.Friends))
                {
                    friends.Rows.Add(characterFriend.FriendName);
                }

                var episodes = new DataTable();
                episodes.Columns.Add("Id", typeof(int));
                episodes.Columns.Add("StringItem", typeof(string));

                foreach (var characterEpisode in characters.SelectMany(s => s.Episodes))
                {
                    episodes.Rows.Add(characterEpisode.EpisodeName);
                }


                var characterId = (await _dbConnection.QueryAsync<int>("[Characters].[InsertCharacter]",
                    new
                    {
                        character.Name,
                        character.Planet,
                        Episodes = episodes.AsTableValuedParameter("dbo.StringValues"),
                        Friends = friends.AsTableValuedParameter("dbo.StringValues")
                    },
                    commandType: CommandType.StoredProcedure)).Single();

                //foreach (var characterEpisode in character.Episodes)
                //{
                //    await _dbConnection.ExecuteAsync("[Characters].[InsertEpisode]",
                //        new
                //        {
                //            Episode = characterEpisode.EpisodeName,
                //            CharacterId = characterId
                //        },
                //        commandType: CommandType.StoredProcedure);
                //}

                //foreach (var characterFriend in character.Friends)
                //{
                //    await _dbConnection.ExecuteAsync("[Characters].[InsertFriend]",
                //        new
                //        {
                //            Friend = characterFriend.FriendName,
                //            CharacterId = characterId
                //        },
                //        commandType: CommandType.StoredProcedure);
                //}
            }
        }

        public async Task UpdateCharacter(int characterId, Character character)
        {
            var episodes = CreateEpisodesNamesDataTable(characterId, character.Episodes);
            var friends = CreateFriendsNamesDataTable(characterId, character.Friends);

            await _dbConnection.ExecuteAsync("[Characters].[UpdateCharacter]",
                new
                {
                    CharacterId = characterId,
                    character.Name,
                    character.Planet,
                    Episodes = episodes.AsTableValuedParameter("dbo.StringValues"),
                    Friends = friends.AsTableValuedParameter("dbo.StringValues")
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteCharacter(int characterId)
        {
            await _dbConnection.ExecuteAsync("[Characters].[DeleteCharacter]",
                new
                {
                    CharacterId = characterId
                });
        }

        private static DataTable CreateFriendsNamesDataTable(int characterId, List<Friend> characterFriends)
        {
            var friends = new DataTable();
            friends.Columns.Add("Id", typeof(int));
            friends.Columns.Add("StringItem", typeof(string));

            foreach (var characterFriend in characterFriends)
            {
                friends.Rows.Add(characterId, characterFriend.FriendName);
            }

            return friends;
        }

        private static DataTable CreateEpisodesNamesDataTable(int characterId, List<Episode> characterEpisodes)
        {
            var episodes = new DataTable();
            episodes.Columns.Add("Id", typeof(int));
            episodes.Columns.Add("StringItem", typeof(string));

            foreach (var characterEpisode in characterEpisodes)
            {
                episodes.Rows.Add(characterId, characterEpisode.EpisodeName);
            }

            return episodes;
        }
    }
}
