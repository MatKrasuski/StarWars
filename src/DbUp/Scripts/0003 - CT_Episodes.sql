IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Characters' AND  TABLE_NAME = 'Episodes')
BEGIN
    CREATE TABLE [Characters].[Episodes](
	    [EpisodeId] [int] IDENTITY(1,1) NOT NULL,
	    [Episode] [varchar](100) NOT NULL,
        [CharacterId] INT NOT NULL
        CONSTRAINT PK_Episodes PRIMARY KEY NONCLUSTERED (EpisodeId)
)
END