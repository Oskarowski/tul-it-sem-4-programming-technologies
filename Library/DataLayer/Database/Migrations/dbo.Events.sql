CREATE TABLE [dbo].[Events]
(
	[guid] VARCHAR(36) NOT NULL PRIMARY KEY, 
    [userGuid] VARCHAR(36) NOT NULL,
    [stateGuid] VARCHAR(36) NOT NULL,
    [type] NVARCHAR(50) NOT NULL,
    [createdAt] DATETIME NOT NULL, 
    CONSTRAINT [FK_Events_ToUsersTable] FOREIGN KEY ([userGuid]) REFERENCES [Users]([guid]),
    CONSTRAINT [DF_Events_ToStatesTable] FOREIGN KEY ([stateGuid]) REFERENCES [States]([guid])
)
