IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[InsertFriend]'))
   exec('CREATE PROCEDURE [Characters].[InsertFriend] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[InsertFriend]
@Friend VARCHAR(100),
@CharacterId INT
AS
 BEGIN
    INSERT INTO Characters.Friends (Friend, CharacterId)
    VALUES(@Friend, @CharacterId)
 END
GO