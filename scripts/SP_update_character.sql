IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[UpdateCharacter]'))
   exec('CREATE PROCEDURE [Characters].[UpdateCharacter] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[UpdateCharacter]
@CharacterId INT,
@Name VARCHAR(50),
@Episodes VARCHAR(max),
@Planet VARCHAR(50),
@Friends VARCHAR(max)

AS
 BEGIN
    UPDATE [Characters].[StarWarsCharacters]
    SET characterName = @Name,
        episodes = @Episodes,
        friends = @Friends,
        planet = @Planet
   
   WHERE characterId = @CharacterId
 END
GO

