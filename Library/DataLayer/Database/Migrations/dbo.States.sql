CREATE TABLE [dbo].[States]
(
	[guid] NCHAR(30) NOT NULL PRIMARY KEY, 
    [productGuid] NCHAR(30) NOT NULL, 
    [quantity] INT NOT NULL, 
    [LastUpdatedDate] DATE NOT NULL, 
    CONSTRAINT [FK_States_ToProductTable] FOREIGN KEY ([guid]) REFERENCES [Books]([guid])
)
