IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Characters' AND  TABLE_NAME = 'StarWarsCharacters')
BEGIN
    CREATE TABLE [Characters].[StarWarsCharacters]
(
    CharacterId int IDENTITY(1,1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Planet VARCHAR(50)
)
END