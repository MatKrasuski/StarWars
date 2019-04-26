IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[GetAllCharacters]'))
   exec('CREATE PROCEDURE [Characters].[GetAllCharacters] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[GetAllCharacters]
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
 END
GO
