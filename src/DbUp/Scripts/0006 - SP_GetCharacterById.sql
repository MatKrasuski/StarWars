IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[GetCharacterById]'))
   exec('CREATE PROCEDURE [Characters].[GetCharacterById] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[GetCharacterById]
@CharacterId INT
AS
 BEGIN
    SELECT 
        swc.CharacterId
        ,swc.Name
        ,swc.Planet
        ,ep.EpisodeId
        ,ep.Episode as 'EpisodeName'
        ,fr.FriendId
        ,fr.Friend as 'FriendName'
    FROM Characters.StarWarsCharacters swc WITH(NOLOCK)
    LEFT JOIN Characters.Episodes ep WITH(NOLOCK)
    ON swc.CharacterId = ep.CharacterId
    LEFT JOIN Characters.Friends fr WITH(NOLOCK)
    ON swc.CharacterId = fr.CharacterId
    WHERE swc.CharacterId = @CharacterId
END
GO