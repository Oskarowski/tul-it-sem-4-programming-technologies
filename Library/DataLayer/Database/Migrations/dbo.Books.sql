CREATE TABLE [dbo].[Books]
(
	[guid] VARCHAR(36) NOT NULL PRIMARY KEY, 
    [name] VARCHAR(50) NOT NULL, 
    [price] FLOAT NOT NULL, 
    [author] VARCHAR(50) NOT NULL, 
    [publisher] VARCHAR(50) NOT NULL, 
    [pages] INT NOT NULL, 
    [publicationDate] DATETIME NOT NULL
)
