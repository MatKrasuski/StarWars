IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[DeleteCharacter]'))
   exec('CREATE PROCEDURE [Characters].[DeleteCharacter] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[DeleteCharacter]
@CharacterId INT

AS
 BEGIN
   DELETE FROM [Characters].[StarWarsCharacters]
   WHERE CharacterId = @CharacterId

   DELETE FROM [Characters].[Episodes]
   WHERE CharacterId = @CharacterId

   DELETE FROM [Characters].[Friends]
   WHERE CharacterId = @CharacterId
 END
GO