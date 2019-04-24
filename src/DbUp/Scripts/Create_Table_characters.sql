CREATE TABLE [Characters].[StarWarsCharacters]
(
    characterId int IDENTITY(1,1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Episodes VARCHAR(max) NOT NULL,
    Planet VARCHAR(50),
    Friends VARCHAR(max) NOT NULL
)