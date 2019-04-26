IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Characters' AND  TABLE_NAME = 'Friends')
BEGIN
    CREATE TABLE [Characters].[Friends](
        [FriendId] [int] IDENTITY(1,1) NOT NULL,
        [Friend] [varchar](100) NOT NULL,
        [CharacterId] INT NOT NULL
        CONSTRAINT PK_Friends PRIMARY KEY NONCLUSTERED (FriendId)
)
END