IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID(N'[Characters].[InsertCharacters]'))
   exec('CREATE PROCEDURE [Characters].[InsertCharacters] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [Characters].[InsertCharacters]
@Name VARCHAR(50),
@Episodes VARCHAR(max),
@Planet VARCHAR(50),
@Friends VARCHAR(max)

AS
 BEGIN
    INSERT INTO [Characters].[StarWarsCharacters] ([Name], episodes, planet, friends)
    VALUES(@Name, @Episodes, @Planet, @Friends)
 END
GO

