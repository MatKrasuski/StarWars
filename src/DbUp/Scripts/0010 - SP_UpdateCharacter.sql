IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[UpdateCharacter]'))
   exec('CREATE PROCEDURE [Characters].[UpdateCharacter] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[UpdateCharacter]
@CharacterId INT,
@Name VARCHAR(50),
@Planet VARCHAR(50),
@Episodes as StringValues READONLY,
@Friends as StringValues READONLY

AS
 BEGIN
    UPDATE Characters.StarWarsCharacters
    SET [Name] = @Name, Planet = @Planet
    WHERE CharacterId = @CharacterId

    DELETE FROM Characters.Episodes
    WHERE CharacterId = @CharacterId

    INSERT INTO Characters.Episodes (Episode, CharacterId)
    SELECT StringItem, Id  FROM @Episodes

    DELETE FROM Characters.Friends
    WHERE CharacterId = @CharacterId

    INSERT INTO Characters.Friends (Friend, CharacterId)
    SELECT StringItem, Id  FROM @Friends
 END
GO