IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[GetCharacters]'))
   exec('CREATE PROCEDURE [Characters].[GetCharacters] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[GetCharacters]
AS
 BEGIN
    SELECT * FROM [Characters].[StarWarsCharacters]
 END
GO