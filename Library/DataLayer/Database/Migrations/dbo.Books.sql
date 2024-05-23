CREATE TABLE [dbo].[Books]
(
	[guid] NCHAR(30) NOT NULL PRIMARY KEY, 
    [name] NCHAR(20) NOT NULL, 
    [price] FLOAT NOT NULL, 
    [author] NCHAR(30) NOT NULL, 
    [publisher] NCHAR(30) NOT NULL, 
    [pages] INT NOT NULL, 
    [publicationDate] DATETIME NOT NULL
)
