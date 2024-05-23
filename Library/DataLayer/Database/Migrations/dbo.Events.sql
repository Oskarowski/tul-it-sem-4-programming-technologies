CREATE TABLE [dbo].[Events]
(
	[guid] NCHAR(30) NOT NULL PRIMARY KEY, 
    [createdAt] DATETIME NOT NULL, 
    [userGuid] NCHAR(30) NOT NULL,
    [stateGuid] NCHAR(30) NOT NULL,
    [type] NVARCHAR(50) NOT NULL,
    CONSTRAINT [FK_Events_ToUsersTable] FOREIGN KEY ([userGuid]) REFERENCES [Users]([guid]),
    CONSTRAINT [DF_Events_ToStatesTable] FOREIGN KEY ([stateGuid]) REFERENCES [States]([guid])
)
