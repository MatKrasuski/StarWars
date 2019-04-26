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

        public async Task<Dictionary<int, CharacterDto>> GetAllCharacters()
        {
            var charactersDictionary = new Dictionary<int, CharacterDto>();

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

            return charactersDictionary;
        }

        public async Task<KeyValuePair<int, CharacterDto>> GetCharacter(int characterId)
        {
            var charactersDictionary = new Dictionary<int, CharacterDto>();

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

            return charactersDictionary.SingleOrDefault();
        }

        public async Task AddCharacters(List<CharacterDto> characters)
        {
            foreach (var character in characters)
            {
                var characterId = (await _dbConnection.QueryAsync<int>("[Characters].[InsertCharacter]",
                    new
                    {
                        character.Name,
                        character.Planet
                        
                    },
                    commandType: CommandType.StoredProcedure)).Single();

                var episodes = CreateEpisodesNamesDataTable(characterId, character.Episodes);
                var friends = CreateFriendsNamesDataTable(characterId, character.Friends);

                await _dbConnection.ExecuteAsync("[Characters].[InsertEpisodesAndFriends]",
                    new
                    {
                        Episodes = episodes.AsTableValuedParameter("dbo.StringValues"),
                        Friends = friends.AsTableValuedParameter("dbo.StringValues")
                    }, 
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateCharacter(CharacterDto character)
        {
            var episodes = CreateEpisodesNamesDataTable(character.CharacterId, character.Episodes);
            var friends = CreateFriendsNamesDataTable(character.CharacterId, character.Friends);

            await _dbConnection.ExecuteAsync("[Characters].[UpdateCharacter]",
                new
                {
                    character.CharacterId,
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
                },
                commandType: CommandType.StoredProcedure);
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
