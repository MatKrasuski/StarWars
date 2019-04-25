IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[InsertCharacter]'))
   exec('CREATE PROCEDURE [Characters].[InsertCharacter] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[InsertCharacter]
@Name VARCHAR(50),
@Planet VARCHAR(50)
AS
 BEGIN
    INSERT INTO Characters.StarWarsCharacters ([Name], Planet)
    OUTPUT inserted.CharacterId
    VALUES (@Name, @Planet)
 END
GO