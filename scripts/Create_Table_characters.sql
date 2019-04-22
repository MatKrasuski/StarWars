CREATE TABLE [Characters].[StarWarsCharacters]
(
    characterId int IDENTITY(1,1) PRIMARY KEY,
    characterName VARCHAR(50) NOT NULL,
    episodes VARCHAR(max) NOT NULL,
    planet VARCHAR(50),
    friends VARCHAR(max) NOT NULL
)