IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[InsertEpisode]'))
   exec('CREATE PROCEDURE [Characters].[InsertEpisode] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[InsertEpisode]
@Episode VARCHAR(100),
@CharacterId INT
AS
 BEGIN
    INSERT INTO Characters.Episodes (Episode, CharacterId)
    VALUES(@Episode, @CharacterId)
 END
GO