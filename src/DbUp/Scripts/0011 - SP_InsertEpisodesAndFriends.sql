IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[InsertEpisodesAndFriends]'))
   exec('CREATE PROCEDURE [Characters].[InsertEpisodesAndFriends] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[InsertEpisodesAndFriends]
@Episodes as StringValues READONLY,
@Friends as StringValues READONLY
AS
 BEGIN
    INSERT INTO Characters.Episodes (Episode, CharacterId)
    SELECT StringItem, Id  FROM @Episodes

    INSERT INTO Characters.Friends (Friend, CharacterId)
    SELECT StringItem, Id  FROM @Friends
 END
GO