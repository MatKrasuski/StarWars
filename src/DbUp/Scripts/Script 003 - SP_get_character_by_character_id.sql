IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[GetCharacterByCharacterId]'))
   exec('CREATE PROCEDURE [Characters].[GetCharacterByCharacterId] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[GetCharacterByCharacterId]
@CharacterId INT
AS
 BEGIN
    SELECT * FROM [Characters].[StarWarsCharacters] with(nolock)
    WHERE characterId = @CharacterId;
 END
GO